using UnityEngine;
using TMPro;

public class ItemInfoUI : MonoBehaviour
{
    public static ItemInfoUI Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => Instance = null;

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemTypeText;
    [SerializeField] private TMP_Text itemWeightText;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Show(ItemBase item, Vector2 screenPos)
    {
        itemNameText.text = item.name;
        itemTypeText.text = item.itemType.ToString();
        itemWeightText.text = $"무게: {item.weight}";
        ((RectTransform)panel.transform).position = screenPos + new Vector2(15f, 15f);
        panel.SetActive(true);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
