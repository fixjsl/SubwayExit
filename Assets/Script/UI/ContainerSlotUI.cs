using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ContainerSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private ContextMenuUI contextMenu;

    [Header("Item Info Panel")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoNameText;
    [SerializeField] private TextMeshProUGUI infoTypeText;
    [SerializeField] private TextMeshProUGUI infoWeightText;

    private System.Action onBeforeShow;

    public void Setup(ItemBase item, int count, Inventory inventory, System.Action onBeforeShow)
    {
        this.onBeforeShow = onBeforeShow;
        icon.sprite = item.icon;
        icon.color = Color.white;
        countText.text = count.ToString();
        contextMenu.Init(item, inventory);

        if (infoNameText != null) infoNameText.text = item.name;
        if (infoTypeText != null) infoTypeText.text = item.itemType.ToString();
        if (infoWeightText != null) infoWeightText.text = $"무게: {item.weight}";
    }

    public void HideContextMenu() => contextMenu.Hide();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onBeforeShow?.Invoke();
            contextMenu.Toggle();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel?.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel?.SetActive(false);
    }
}
