using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ContainerSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;

    [Header("Item Info Panel")]
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoNameText;
    [SerializeField] private TextMeshProUGUI infoTypeText;
    [SerializeField] private TextMeshProUGUI infoWeightText;

    private System.Action onLeftClick;
    private System.Action onRightClick;
    private ItemBase currentItem;

    public void Setup(ItemBase item, int count, System.Action onLeft, System.Action onRight = null)
    {
        currentItem = item;
        icon.sprite = item.icon;
        countText.text = count.ToString();
        onLeftClick = onLeft;
        onRightClick = onRight;

        if (infoNameText != null) infoNameText.text = item.name;
        if (infoTypeText != null) infoTypeText.text = item.itemType.ToString();
        if (infoWeightText != null) infoWeightText.text = $"무게: {item.weight}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            onLeftClick?.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            onRightClick?.Invoke();
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
