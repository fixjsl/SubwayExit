using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject settingsPanel;

    void Awake()
    {
        startButton.onClick.AddListener(OnStart);
        settingsButton.onClick.AddListener(OnSettings);
        quitButton.onClick.AddListener(OnQuit);
    }

    void OnStart()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    void OnSettings()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
