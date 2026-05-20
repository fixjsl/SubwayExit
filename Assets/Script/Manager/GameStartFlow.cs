using UnityEngine;
using UnityEngine.UI;

public class GameStartFlow : MonoBehaviour
{
    public static GameStartFlow Instance { get; private set; }

    [Header("Tutorial Pages")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Button nextButton;
    [SerializeField] private Sprite[] pages;

    [Header("Weapon Selection")]
    [SerializeField] private GameObject weaponCardContainer;

    private int currentPage = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        nextButton.onClick.AddListener(OnNext);
    }

    void Start()
    {
        Time.timeScale = 0f;
        PlayerStateMachine.Instance.SetMovementLocked(true);

        weaponCardContainer.SetActive(false);
        ShowPage(0);
        tutorialPanel.SetActive(true);
    }

    void ShowPage(int index)
    {
        tutorialImage.sprite = pages[index];
    }

    void OnNext()
    {
        currentPage++;
        if (currentPage < pages.Length)
            ShowPage(currentPage);
        else
        {
            Debug.Log("dnpdksehladnpdksaehal");
            tutorialPanel.SetActive(false);
            weaponCardContainer.SetActive(true);
        }
    }

    public void OnWeaponSelected()
    {
        weaponCardContainer.SetActive(false);
        PlayerStateMachine.Instance.SetMovementLocked(false);
        Time.timeScale = 1f;
        GameManager.Instance.TutorialStart();
    }
}
