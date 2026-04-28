using System;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    public static ContainerUI Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private Transform slotParent;
    [SerializeField] private ContainerSlotUI slotPrefab;

    private List<(ItemBase item, int count)> currentContents;
    private Action onContentsChanged;
    private readonly List<ContainerSlotUI> activeSlots = new();

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void Open(List<(ItemBase item, int count)> contents, Action onContentsChanged)
    {
        currentContents = contents;
        this.onContentsChanged = onContentsChanged;
        Refresh();
        panel.SetActive(true);
        InventoryUI.Instance.Open();
    }

    public void Close()
    {
        panel.SetActive(false);
        currentContents = null;
        onContentsChanged = null;
        InventoryUI.Instance.Close();
    }

    public void TakeItem(int index)
    {
        if (currentContents == null || index >= currentContents.Count) return;
        var (item, count) = currentContents[index];

        if (!PlayerStateMachine.Instance.inventory.AddItem(item, count)) return;

        currentContents.RemoveAt(index);
        onContentsChanged?.Invoke();
        Refresh();
    }

    private void Refresh()
    {
        foreach (var slot in activeSlots)
            Destroy(slot.gameObject);
        activeSlots.Clear();

        for (int i = 0; i < currentContents.Count; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            int idx = i;
            slot.Setup(currentContents[i].item, currentContents[i].count, () => TakeItem(idx));
            activeSlots.Add(slot);
        }
    }
}
