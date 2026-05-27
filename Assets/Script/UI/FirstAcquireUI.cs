using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FirstAcquireUI : MonoBehaviour
{
    public static FirstAcquireUI Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => Instance = null;

    [SerializeField] private GameObject panel;
    [SerializeField] private Image displayImage;
    [SerializeField] private Button closeButton;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        closeButton.onClick.AddListener(Close);
    }

    void Start()
    {
        PlayerStateMachine.Instance.inventory.OnFirstAcquire += Show;
    }

    void Update()
    {
        if (panel.activeSelf && Keyboard.current.escapeKey.wasPressedThisFrame)
            Close();
    }

    private void Show(ItemBase item)
    {
        displayImage.sprite = item.firstAcquireImage;
        panel.SetActive(true);
        SetCombatInput(false);
    }

    private void Close()
    {
        panel.SetActive(false);
        SetCombatInput(true);
    }

    private void SetCombatInput(bool enabled)
    {
        System.Action<InputAction> toggle = enabled ? a => a.Enable() : a => a.Disable();
        toggle(InputBindings.GetAction("Attack"));
        toggle(InputBindings.GetAction("Dodge"));
        toggle(InputBindings.GetAction("Guard"));
    }

    void OnDestroy()
    {
        if (PlayerStateMachine.Instance != null)
            PlayerStateMachine.Instance.inventory.OnFirstAcquire -= Show;
    }
}
