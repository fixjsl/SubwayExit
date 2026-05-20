using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject StartMenuPanel;
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Button     BackButton;

    [Header("Intro Video")]
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private Button skipButton;

    void Awake()
    {
        startButton.onClick.AddListener(OnStart);
        settingsButton.onClick.AddListener(OnSettings);
        quitButton.onClick.AddListener(OnQuit);
        BackButton.onClick.AddListener(OnExitSetting);

        if (skipButton != null)
        {
            skipButton.onClick.AddListener(LoadGameScene);
            skipButton.gameObject.SetActive(false);
        }

        if (videoPlayer != null)
            videoPlayer.loopPointReached += _ => LoadGameScene();
    }

    void OnStart()
    {
        if (videoPlayer == null || videoPlayer.clip == null)
        {
            LoadGameScene();
            return;
        }

        StartMenuPanel.SetActive(false);
        videoPanel.SetActive(true);
        skipButton.gameObject.SetActive(true);
        videoPlayer.Play();
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("Base&Main1");
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
