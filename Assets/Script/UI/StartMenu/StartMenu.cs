using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMenuPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button     BackButton;

    void Awake()
    {
        startButton.onClick.AddListener(OnStart);
        settingsButton.onClick.AddListener(OnSettings);
        quitButton.onClick.AddListener(OnQuit);
        BackButton.onClick.AddListener(OnExitSetting);
    }

    void OnStart()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    void OnSettings()
    {
        StartMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
    void OnExitSetting()
    {
        StartMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
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
