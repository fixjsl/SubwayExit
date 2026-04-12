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

    public bool AddItem(Item item, int num = 1)
    {
        int key = item.iteminfo.itemcode;
        float addWeight = item.iteminfo.weight * num;
        if (currentWeight + addWeight > status.curMaxCarryWeight) return false;

        if (slots.TryGetValue(key, out int count))
        {
            slots[key] = num + count;
            currentWeight += addWeight;
            return true;
        }
        if (slots.Count >= (int)status.maxSlots) return false;
        slots[key] = num;
        currentWeight += addWeight;
        return true;
    }

    public bool RemoveItem(Item item, int num = 1)
    {
        int key = item.iteminfo.itemcode;
        if (!slots.TryGetValue(key, out int count)) return false;
        if (count < num) return false;

        if (count == num)
            slots.Remove(key);
        else
            slots[key] = count - num;
        currentWeight -= item.iteminfo.weight * num;
        return true;
    }

}
