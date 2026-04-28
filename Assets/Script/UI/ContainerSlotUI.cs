using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContainerSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private Button button;

    public void Setup(ItemBase item, int count, System.Action onClick)
    {
        Debug.Log(icon.sprite.name);
        icon.sprite = item.icon;

        countText.text = count > 1 ? count.ToString() : "";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick?.Invoke());
    }
}
