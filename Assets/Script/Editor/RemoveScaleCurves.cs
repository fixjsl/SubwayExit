#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

public class RemoveScaleCurves : EditorWindow
{
    // FBX 선택 → 클립 추출 + 스케일 커브 제거해서 .anim으로 저장
    [MenuItem("Tools/Extract Clips & Remove Scale Curves")]
    static void ExtractAndRemove()
    {
        int count = 0;
        foreach (var obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (!path.EndsWith(".fbx", System.StringComparison.OrdinalIgnoreCase)) continue;

            string dir = Path.GetDirectoryName(path);
            var allAssets = AssetDatabase.LoadAllAssetsAtPath(path);

            foreach (var asset in allAssets)
            {
                if (!(asset is AnimationClip src) || src.name.Contains("__preview__")) continue;

                var newClip = new AnimationClip();
                newClip.name = src.name;
                EditorUtility.CopySerialized(src, newClip);

                // 스케일 커브 제거
                var bindings = AnimationUtility.GetCurveBindings(newClip);
                foreach (var b in bindings)
                {
                    if (b.propertyName.StartsWith("m_LocalScale"))
                        AnimationUtility.SetEditorCurve(newClip, b, null);
                }

                string clipName = src.name.Replace("|", "_").Replace(" ", "_");
                string savePath = Path.Combine(dir, clipName + ".anim").Replace("\\", "/");
                AssetDatabase.CreateAsset(newClip, savePath);
                count++;
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log($"[RemoveScaleCurves] {count}개 클립 추출 + Scale 커브 제거 완료");
    }

    // .anim 파일 직접 선택했을 때 스케일 커브만 제거
    [MenuItem("Tools/Remove Scale Curves from Selected Clips")]
    static void RemoveOnly()
    {
        int count = 0;
        foreach (var obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            var clips = AssetDatabase.LoadAllAssetsAtPath(path);
            foreach (var asset in clips)
            {
                if (!(asset is AnimationClip clip) || clip.name.Contains("__preview__")) continue;
                RemoveScale(clip);
                count++;
            }
        }
        AssetDatabase.SaveAssets();
        Debug.Log($"[RemoveScaleCurves] {count}개 클립에서 Scale 커브 제거 완료");
    }

    static void RemoveScale(AnimationClip clip)
    {
        var bindings = AnimationUtility.GetCurveBindings(clip);
        foreach (var binding in bindings)
        {
            if (binding.propertyName.StartsWith("m_LocalScale"))
                AnimationUtility.SetEditorCurve(clip, binding, null);
        }
    }
}
#endif
