using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private Transform slotParent;
    [SerializeField] private ContainerSlotUI slotPrefab;
    [SerializeField] private Image weightFill;
    [SerializeField] private TMP_Text weightText;
    [SerializeField] private ContextMenuUI contextMenu;

    private Inventory inventory;
    private readonly List<ContainerSlotUI> activeSlots = new();
    private InputAction _inventoryAction;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        inventory = PlayerStateMachine.Instance.inventory;
        inventory.OnInventoryChanged += Refresh;
        panel.SetActive(false);

        _inventoryAction = InputBindings.GetAction("Inventory");
        Debug.Log($"인벤토리 액션: {(_inventoryAction == null ? "null" : "정상")}");
        if (_inventoryAction != null)
            _inventoryAction.performed += OnInventoryPerformed;
    }

    private void OnInventoryPerformed(InputAction.CallbackContext ctx)
    {
        Debug.Log("인벤토리 입력 들어옴");
        Toggle();
    }

    void Toggle()
    {
        if (panel.activeSelf) Close();
        else Open();
    }

    public void Open()
    {
        PositionByPlayerSide();
        panel.SetActive(true);
        Refresh();
        contextMenu.Hide();
    }

    [SerializeField] private float inventorySideOffset = 635f;

    private void PositionByPlayerSide()
    {
        float viewportX = Camera.main.WorldToViewportPoint(PlayerStateMachine.Instance.transform.position).x;
        var rt = (RectTransform)panel.transform;

        // anchor/pivot은 인스펙터에서 center(0.5, 0.5)로 고정, 위치만 변경
        if (viewportX < 0.5f)
            rt.anchoredPosition = new Vector2(inventorySideOffset, 0f);   // 플레이어 왼쪽 → 인벤 오른쪽
        else
            rt.anchoredPosition = new Vector2(-inventorySideOffset, 0f);  // 플레이어 오른쪽 → 인벤 왼쪽
    }

    public void Close()
    {
        panel.SetActive(false);
        contextMenu.Hide();
    }

    void Refresh()
    {
        foreach (var slot in activeSlots)
            Destroy(slot.gameObject);
        activeSlots.Clear();

        foreach (var (code, count) in inventory.slots)
        {
            if (!ItemManager.itemDB.TryGetValue(code, out var item)) continue;
            var slot = Instantiate(slotPrefab, slotParent);
            var capturedItem = item;
            slot.Setup(item, count, () => ShowContextMenu(capturedItem));
            activeSlots.Add(slot);
        }

        var status = PlayerStateMachine.Instance.status;
        float max = status.curMaxCarryWeight;
        float current = inventory.currentWeight;
        if (weightFill != null) weightFill.fillAmount = max > 0 ? current / max : 0f;
        if (weightText != null) weightText.text = $"{current:F1} / {max:F1}";
    }

    public void ShowContextMenu(ItemBase item)
    {
        Vector2 pos = Mouse.current.position.ReadValue();
        contextMenu.Show(item, pos, inventory);
    }

    void OnDestroy()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= Refresh;
        if (_inventoryAction != null)
            _inventoryAction.performed -= OnInventoryPerformed;
    }
}
