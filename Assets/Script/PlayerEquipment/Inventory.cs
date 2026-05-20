using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private PlayerStatus status;
    public Dictionary<int, int> slots { get; private set; } = new Dictionary<int, int>();
    public float currentWeight { get; private set; }
    public int[] QuickSlots { get; private set; } = new int[2];

    public event Action OnInventoryChanged;
    public event Action OnQuickSlotsChanged;

    public Inventory(PlayerStatus status)
    {
        this.status = status;
    }

    public bool AddItem(ItemBase itemBase, int num = 1)
    {
        int key = itemBase.itemcode;
        float addWeight = itemBase.weight * num;
        if (currentWeight + addWeight > status.curMaxCarryWeight) return false;

        if (slots.TryGetValue(key, out int count))
        {
            slots[key] = count + num;
            currentWeight += addWeight;
            OnInventoryChanged?.Invoke();
            return true;
        }
        if (slots.Count >= (int)status.maxSlots) return false;
        slots[key] = num;
        currentWeight += addWeight;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public bool RemoveItem(ItemBase itemBase, int num = 1)
    {
        int key = itemBase.itemcode;
        if (!slots.TryGetValue(key, out int count)) return false;
        if (count < num) return false;

        if (count == num)
        {
            slots.Remove(key);
            ClearQuickSlotIfEmpty(key);
        }
        else
            slots[key] = count - num;

        currentWeight -= itemBase.weight * num;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void UseItem(ItemBase itemBase, PlayerStateMachine player)
    {
        if (!slots.ContainsKey(itemBase.itemcode)) return;
        itemBase.OnUse(player);
        RemoveItem(itemBase, 1);
    }

    public void DropItem(ItemBase itemBase, int num = 1) => RemoveItem(itemBase, num);

    public void SetQuickSlot(int slotIndex, int itemCode)
    {
        if (slotIndex < 0 || slotIndex >= QuickSlots.Length) return;
        QuickSlots[slotIndex] = itemCode;
        OnQuickSlotsChanged?.Invoke();
    }

    public void UseQuickSlot(int slotIndex, PlayerStateMachine player)
    {
        if (slotIndex < 0 || slotIndex >= QuickSlots.Length) return;
        int code = QuickSlots[slotIndex];
        if (code == 0 || !slots.ContainsKey(code)) { QuickSlots[slotIndex] = 0; return; }
        if (!ItemManager.itemDB.TryGetValue(code, out var item))
        {
            Debug.LogWarning($"[Inventory] itemcode {code} not found in itemDB");
            QuickSlots[slotIndex] = 0;
            OnQuickSlotsChanged?.Invoke();
            return;
        }
        UseItem(item, player);
        OnQuickSlotsChanged?.Invoke();
    }

    private void ClearQuickSlotIfEmpty(int itemCode)
    {
        for (int i = 0; i < QuickSlots.Length; i++)
        {
            if (QuickSlots[i] == itemCode)
            {
                QuickSlots[i] = 0;
                OnQuickSlotsChanged?.Invoke();
            }
        }
    }
}
