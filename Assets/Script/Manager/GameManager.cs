using System.IO;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class GameSettings
{
    public int resolutionIndex  = -1;   // -1 = 현재 해상도 사용
    public int windowMode       = 0;    // 0=전체화면, 1=보더리스, 2=창모드
    public int qualityLevel     = 2;    // 0=낮음, 1=중간, 2=높음
    public float masterVolume   = 1f;
    public float bgmVolume      = 1f;
    public float sfxVolume      = 1f;
    public float ambientVolume  = 1f;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameSettings settings { get; private set; } = new GameSettings();

    [SerializeField] private AudioMixer audioMixer;

    private string savePath => Path.Combine(Application.persistentDataPath, "settings.json");

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadSettings();
        ApplySettings();
    }

    public void SaveSettings()
    {
        File.WriteAllText(savePath, JsonUtility.ToJson(settings, true));
    }

    void LoadSettings()
    {
        if (File.Exists(savePath))
            JsonUtility.FromJsonOverwrite(File.ReadAllText(savePath), settings);
        // 파일 없으면 GameSettings 기본값 그대로 사용
    }

    public void ApplySettings()
    {
        // 해상도 & 창모드
        Resolution[] resolutions = Screen.resolutions;
        int index = settings.resolutionIndex >= 0 && settings.resolutionIndex < resolutions.Length
            ? settings.resolutionIndex
            : resolutions.Length - 1;

        FullScreenMode mode = settings.windowMode switch
        {
            0 => FullScreenMode.ExclusiveFullScreen,
            1 => FullScreenMode.FullScreenWindow,
            _ => FullScreenMode.Windowed
        };
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, mode);

        // 품질
        QualitySettings.SetQualityLevel(settings.qualityLevel);

        // 오디오
        SetMixerVolume("Master",  settings.masterVolume);
        SetMixerVolume("BGM",     settings.bgmVolume);
        SetMixerVolume("SFX",     settings.sfxVolume);
        SetMixerVolume("Ambient", settings.ambientVolume);
    }

    void SetMixerVolume(string param, float value)
    {
        audioMixer.SetFloat(param, Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f);
    }
}
