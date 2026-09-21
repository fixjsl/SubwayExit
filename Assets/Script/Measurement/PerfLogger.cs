using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 성능 측정용 로거.
/// 프레임별 지표를 수집하여 CSV 두 개(원시 표본, 요약)를 저장한다.
/// 개발 빌드(Development Build)에서만 대부분의 지표가 수집된다.
///
/// 사용법
///  1. 측정할 씬의 빈 게임오브젝트에 붙인다.
///  2. 실행 후 F9로 측정을 시작한다. durationSeconds가 지나면 자동 종료된다.
///  3. 저장 경로는 화면과 로그에 표시된다.
///
/// 빌드를 다시 만들지 않고 조건을 구분하려면 실행 인자를 쓴다.
///   SubwayExit.exe -label ssao_on
/// </summary>
public class PerfLogger : MonoBehaviour
{
    [Header("측정 설정")]
    [Tooltip("파일 이름에 붙는 구분자. 실행 인자 -label 이 있으면 그 값이 우선한다.")]
    public string label = "run";

    [Tooltip("측정 시작 후 이 시간(초)의 표본은 버린다. 셰이더 컴파일과 캐시 예열 구간.")]
    public float warmupSeconds = 5f;

    [Tooltip("이 시간(초)이 지나면 자동 종료한다. 0이면 F9로 직접 종료.")]
    public float durationSeconds = 30f;

    [Tooltip("측정 시작/종료 키.")]
    public Key toggleKey = Key.F9;

    [Tooltip("화면에 현재 상태를 표시한다.")]
    public bool showOverlay = true;

    // ─── 수집 상태 ──────────────────────────────────────────────
    struct Sample
    {
        public float time;
        public float frameMs;
        public long drawCalls, batches, setPass, triangles, vertices;
        public long gcAllocBytes, gcReservedBytes, totalReservedBytes;
        public double flashPassMs, blitPassMs;
    }

    readonly List<Sample> samples = new List<Sample>(20000);
    bool recording;
    float startTime;
    string lastSavedPath = "";

    // ─── 프로파일러 카운터 ──────────────────────────────────────
    ProfilerRecorder rDrawCalls, rBatches, rSetPass, rTriangles, rVertices;
    ProfilerRecorder rGcAlloc, rGcReserved, rTotalReserved;
    ProfilerRecorder rFlashPass, rBlitPass;

    void Awake()
    {
        // 실행 인자가 있으면 라벨을 덮어쓴다.
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length - 1; i++)
        {
            if (args[i] == "-label") { label = args[i + 1]; break; }
        }
    }

    void OnDisable()
    {
        StopRecorders();
    }

    void StartRecorders()
    {
        // 렌더링 카운터
        rDrawCalls  = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
        rBatches    = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Batches Count");
        rSetPass    = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
        rTriangles  = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
        rVertices   = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");

        // 메모리 카운터
        rGcAlloc        = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Allocated In Frame");
        rGcReserved     = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        rTotalReserved  = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");

        // 사용자 정의 패스 마커. 한 프레임에 여러 번 찍힐 수 있으므로 합산 옵션을 준다.
        rFlashPass = ProfilerRecorder.StartNew(
            ProfilerCategory.Render, "FlashlightPass", 1, ProfilerRecorderOptions.SumAllSamplesInFrame);
        rBlitPass = ProfilerRecorder.StartNew(
            ProfilerCategory.Render, "Blit", 1, ProfilerRecorderOptions.SumAllSamplesInFrame);
    }

    void StopRecorders()
    {
        rDrawCalls.Dispose();  rBatches.Dispose();     rSetPass.Dispose();
        rTriangles.Dispose();  rVertices.Dispose();
        rGcAlloc.Dispose();    rGcReserved.Dispose();  rTotalReserved.Dispose();
        rFlashPass.Dispose();  rBlitPass.Dispose();
    }

    static long Val(ProfilerRecorder r) => r.Valid ? r.LastValue : -1;
    static double Ms(ProfilerRecorder r) => r.Valid ? r.LastValue * 1e-6 : -1.0; // ns → ms

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current[toggleKey].wasPressedThisFrame)
        {
            if (recording) Stop();
            else Begin();
        }

        if (!recording) return;

        float elapsed = Time.realtimeSinceStartup - startTime;

        if (elapsed >= warmupSeconds)
        {
            samples.Add(new Sample
            {
                time               = elapsed - warmupSeconds,
                frameMs            = Time.unscaledDeltaTime * 1000f,
                drawCalls          = Val(rDrawCalls),
                batches            = Val(rBatches),
                setPass            = Val(rSetPass),
                triangles          = Val(rTriangles),
                vertices           = Val(rVertices),
                gcAllocBytes       = Val(rGcAlloc),
                gcReservedBytes    = Val(rGcReserved),
                totalReservedBytes = Val(rTotalReserved),
                flashPassMs        = Ms(rFlashPass),
                blitPassMs         = Ms(rBlitPass),
            });
        }

        if (durationSeconds > 0f && elapsed >= warmupSeconds + durationSeconds)
            Stop();
    }

    void Begin()
    {
        samples.Clear();
        StartRecorders();
        startTime = Time.realtimeSinceStartup;
        recording = true;
        lastSavedPath = "";
        Debug.Log($"[PerfLogger] 측정 시작. 예열 {warmupSeconds}초 후 표본 수집.");
    }

    void Stop()
    {
        recording = false;
        Save();
        StopRecorders();
    }

    // ─── 저장 ───────────────────────────────────────────────────
    void Save()
    {
        if (samples.Count == 0)
        {
            Debug.LogWarning("[PerfLogger] 표본이 없습니다.");
            return;
        }

        var ci = CultureInfo.InvariantCulture;
        string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string dir = Path.Combine(Application.persistentDataPath, "perf");
        Directory.CreateDirectory(dir);

        // 1) 원시 표본
        var raw = new StringBuilder(samples.Count * 96);
        raw.AppendLine("frame,time_s,frame_ms,fps,draw_calls,batches,setpass_calls," +
                       "triangles,vertices,gc_alloc_bytes,gc_reserved_bytes," +
                       "total_reserved_bytes,flashlight_pass_ms,blit_pass_ms");
        for (int i = 0; i < samples.Count; i++)
        {
            var s = samples[i];
            raw.Append(i).Append(',')
               .Append(s.time.ToString("F4", ci)).Append(',')
               .Append(s.frameMs.ToString("F4", ci)).Append(',')
               .Append((1000f / Mathf.Max(s.frameMs, 1e-4f)).ToString("F2", ci)).Append(',')
               .Append(s.drawCalls).Append(',')
               .Append(s.batches).Append(',')
               .Append(s.setPass).Append(',')
               .Append(s.triangles).Append(',')
               .Append(s.vertices).Append(',')
               .Append(s.gcAllocBytes).Append(',')
               .Append(s.gcReservedBytes).Append(',')
               .Append(s.totalReservedBytes).Append(',')
               .Append(s.flashPassMs.ToString("F4", ci)).Append(',')
               .Append(s.blitPassMs.ToString("F4", ci)).Append('\n');
        }
        string rawPath = Path.Combine(dir, $"raw_{label}_{stamp}.csv");
        File.WriteAllText(rawPath, raw.ToString(), Encoding.UTF8);

        // 2) 요약
        var frameMs = new List<float>(samples.Count);
        var flash   = new List<double>(samples.Count);
        var blit    = new List<double>(samples.Count);
        long sumDraw = 0, sumBatch = 0, sumSetPass = 0, sumTri = 0, sumVert = 0, sumGcAlloc = 0;
        long maxGcReserved = 0, maxTotalReserved = 0;

        foreach (var s in samples)
        {
            frameMs.Add(s.frameMs);
            if (s.flashPassMs >= 0) flash.Add(s.flashPassMs);
            if (s.blitPassMs  >= 0) blit.Add(s.blitPassMs);
            sumDraw += s.drawCalls;   sumBatch += s.batches;  sumSetPass += s.setPass;
            sumTri  += s.triangles;   sumVert  += s.vertices;
            if (s.gcAllocBytes > 0) sumGcAlloc += s.gcAllocBytes;
            maxGcReserved    = Math.Max(maxGcReserved, s.gcReservedBytes);
            maxTotalReserved = Math.Max(maxTotalReserved, s.totalReservedBytes);
        }

        frameMs.Sort();
        int n = frameMs.Count;
        float meanMs = 0f; foreach (var v in frameMs) meanMs += v; meanMs /= n;
        float medMs  = frameMs[n / 2];
        float p95Ms  = frameMs[Mathf.Min(n - 1, (int)(n * 0.95f))];
        float p99Ms  = frameMs[Mathf.Min(n - 1, (int)(n * 0.99f))];

        var sum = new StringBuilder();
        sum.AppendLine("항목,값");
        sum.AppendLine($"라벨,{label}");
        sum.AppendLine($"측정 시각,{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        sum.AppendLine($"품질 레벨,{QualitySettings.names[QualitySettings.GetQualityLevel()]}");
        sum.AppendLine($"해상도,{Screen.width}x{Screen.height}");
        sum.AppendLine($"수직 동기화,{QualitySettings.vSyncCount}");
        sum.AppendLine($"목표 프레임,{Application.targetFrameRate}");
        sum.AppendLine($"개발 빌드,{Debug.isDebugBuild}");
        sum.AppendLine($"그래픽 API,{SystemInfo.graphicsDeviceType}");
        sum.AppendLine($"GPU,{SystemInfo.graphicsDeviceName}");
        sum.AppendLine($"CPU,{SystemInfo.processorType}");
        sum.AppendLine($"시스템 메모리 MB,{SystemInfo.systemMemorySize}");
        sum.AppendLine($"유니티 버전,{Application.unityVersion}");
        sum.AppendLine($"표본 수,{n}");
        sum.AppendLine($"예열 초,{warmupSeconds.ToString(ci)}");
        sum.AppendLine($"측정 초,{durationSeconds.ToString(ci)}");
        sum.AppendLine();
        sum.AppendLine($"평균 FPS,{(1000f / meanMs).ToString("F2", ci)}");
        sum.AppendLine($"중앙 FPS,{(1000f / medMs).ToString("F2", ci)}");
        sum.AppendLine($"평균 프레임 시간 ms,{meanMs.ToString("F3", ci)}");
        sum.AppendLine($"중앙 프레임 시간 ms,{medMs.ToString("F3", ci)}");
        sum.AppendLine($"95퍼센타일 프레임 시간 ms,{p95Ms.ToString("F3", ci)}");
        sum.AppendLine($"99퍼센타일 프레임 시간 ms,{p99Ms.ToString("F3", ci)}");
        sum.AppendLine($"최소 프레임 시간 ms,{frameMs[0].ToString("F3", ci)}");
        sum.AppendLine($"최대 프레임 시간 ms,{frameMs[n - 1].ToString("F3", ci)}");
        sum.AppendLine();
        sum.AppendLine($"평균 드로우콜,{(sumDraw / (double)n).ToString("F1", ci)}");
        sum.AppendLine($"평균 배치,{(sumBatch / (double)n).ToString("F1", ci)}");
        sum.AppendLine($"평균 SetPass 호출,{(sumSetPass / (double)n).ToString("F1", ci)}");
        sum.AppendLine($"평균 삼각형 수,{(sumTri / (double)n).ToString("F0", ci)}");
        sum.AppendLine($"평균 정점 수,{(sumVert / (double)n).ToString("F0", ci)}");
        sum.AppendLine();
        sum.AppendLine($"프레임당 GC 할당 바이트 평균,{(sumGcAlloc / (double)n).ToString("F1", ci)}");
        sum.AppendLine($"GC 예약 메모리 최대 MB,{(maxGcReserved / 1048576.0).ToString("F2", ci)}");
        sum.AppendLine($"전체 예약 메모리 최대 MB,{(maxTotalReserved / 1048576.0).ToString("F2", ci)}");
        sum.AppendLine();
        AppendMarker(sum, ci, "조명 합성 패스", flash);
        AppendMarker(sum, ci, "화면 복사 패스", blit);

        string sumPath = Path.Combine(dir, $"summary_{label}_{stamp}.csv");
        File.WriteAllText(sumPath, sum.ToString(), Encoding.UTF8);

        lastSavedPath = dir;
        Debug.Log($"[PerfLogger] 저장 완료\n{rawPath}\n{sumPath}");
    }

    static void AppendMarker(StringBuilder sb, CultureInfo ci, string name, List<double> v)
    {
        if (v.Count == 0)
        {
            sb.AppendLine($"{name} ms,수집 실패 (마커 없음)");
            return;
        }
        v.Sort();
        double mean = 0; foreach (var x in v) mean += x; mean /= v.Count;
        sb.AppendLine($"{name} 평균 ms,{mean.ToString("F4", ci)}");
        sb.AppendLine($"{name} 중앙 ms,{v[v.Count / 2].ToString("F4", ci)}");
        sb.AppendLine($"{name} 최대 ms,{v[v.Count - 1].ToString("F4", ci)}");
    }

    // ─── 화면 표시 ──────────────────────────────────────────────
    void OnGUI()
    {
        if (!showOverlay) return;

        var style = new GUIStyle(GUI.skin.label)
        {
            fontSize = 16,
            normal = { textColor = Color.white }
        };

        GUI.Box(new Rect(8, 8, 420, recording ? 110 : 90), GUIContent.none);
        GUILayout.BeginArea(new Rect(16, 14, 404, 110));

        if (recording)
        {
            float elapsed = Time.realtimeSinceStartup - startTime;
            bool warming = elapsed < warmupSeconds;
            GUILayout.Label(warming
                ? $"예열 중 {elapsed:F1} / {warmupSeconds:F0}초"
                : $"측정 중 {elapsed - warmupSeconds:F1}초   표본 {samples.Count}", style);
            GUILayout.Label($"FPS {1f / Mathf.Max(Time.unscaledDeltaTime, 1e-5f):F1}   " +
                            $"배치 {Val(rBatches)}   삼각형 {Val(rTriangles)}", style);
            GUILayout.Label($"조명 패스 {Ms(rFlashPass):F3} ms", style);
        }
        else
        {
            GUILayout.Label($"[{toggleKey}] 측정 시작   라벨: {label}", style);
            GUILayout.Label($"품질: {QualitySettings.names[QualitySettings.GetQualityLevel()]}   " +
                            $"개발 빌드: {Debug.isDebugBuild}", style);
            if (!string.IsNullOrEmpty(lastSavedPath))
                GUILayout.Label($"저장됨: {lastSavedPath}", style);
        }

        GUILayout.EndArea();
    }
}
