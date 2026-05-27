using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingMaterialUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text countText;

    public void Setup(ItemBase item, int required, int current)
    {
        if (icon != null) icon.sprite = item.icon;
        if (countText != null)
        {
            countText.text = $"{current} / {required}";
            countText.color = current >= required ? Color.white : Color.red;
        }
    }
}
