using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private PlayerStatus status;
    public Dictionary<int, int> slots { get; private set; } = new Dictionary<int, int>();
    public float currentWeight { get; private set; }

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
            return true;
        }
        if (slots.Count >= (int)status.maxSlots) return false;
        slots[key] = num;
        currentWeight += addWeight;
        return true;
    }

    public bool AddItem(Item item, int num = 1) => AddItem(item.iteminfo, num);

    public bool RemoveItem(ItemBase itemBase, int num = 1)
    {
        int key = itemBase.itemcode;
        if (!slots.TryGetValue(key, out int count)) return false;
        if (count < num) return false;

        if (count == num)
            slots.Remove(key);
        else
            slots[key] = count - num;
        currentWeight -= itemBase.weight * num;
        return true;
    }

    public bool RemoveItem(Item item, int num = 1) => RemoveItem(item.iteminfo, num);

    public void UseItem(ItemBase itemBase, PlayerStateMachine player)
    {
        if (!slots.ContainsKey(itemBase.itemcode)) return;
        itemBase.OnUse(player);
        RemoveItem(itemBase, 1);
    }

}
