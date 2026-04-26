using System;
using System.Collections.Generic;
using UnityEngine;

public class ContainerUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform slotParent;
    [SerializeField] private ContainerSlotUI slotPrefab;

    private List<(ItemBase item, int count)> currentContents;
    private Action onContentsChanged;
    private readonly List<ContainerSlotUI> activeSlots = new();

    void Awake()
    {
        panel.SetActive(false);
    }

    public void Open(List<(ItemBase item, int count)> contents, Action onContentsChanged)
    {
        currentContents = contents;
        this.onContentsChanged = onContentsChanged;
        Refresh();
        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
        currentContents = null;
        onContentsChanged = null;
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
            slot.Setup(currentContents[i].item, currentContents[i].count, i, this);
            activeSlots.Add(slot);
        }
    }
}
