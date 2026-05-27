using UnityEngine;
using UnityEngine.UI;

public class ContextMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button quickSlot1Btn;
    [SerializeField] private Button quickSlot2Btn;
    [SerializeField] private Button useBtn;
    [SerializeField] private Button dropBtn;

    private ItemBase currentItem;
    private Inventory inventory;
    private System.Action onUseOverride;

    void Awake()
    {
        panel.SetActive(false);
        quickSlot1Btn.onClick.AddListener(() => AssignQuickSlot(0));
        quickSlot2Btn.onClick.AddListener(() => AssignQuickSlot(1));
        useBtn.onClick.AddListener(Use);
        dropBtn.onClick.AddListener(Drop);
    }

    public void Show(ItemBase item, Vector2 screenPos, Inventory inv, System.Action onUse = null, bool showDrop = true)
    {
        currentItem = item;
        inventory = inv;
        onUseOverride = onUse;
        ((RectTransform)panel.transform).position = screenPos;

        bool isConsumable = item.itemType == ItemType.Consumable;
        quickSlot1Btn.gameObject.SetActive(isConsumable);
        quickSlot2Btn.gameObject.SetActive(isConsumable);
        useBtn.gameObject.SetActive(isConsumable);
        dropBtn.gameObject.SetActive(showDrop);

        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
        currentItem = null;
    }

    void AssignQuickSlot(int index)
    {
        inventory.SetQuickSlot(index, currentItem.itemcode);
        Hide();
    }

    void Use()
    {
        if (onUseOverride != null)
            onUseOverride.Invoke();
        else
            inventory.UseItem(currentItem, PlayerStateMachine.Instance);
        Hide();
    }

    void Drop()
    {
        inventory.DropItem(currentItem, 1);
        Hide();
    }
}
