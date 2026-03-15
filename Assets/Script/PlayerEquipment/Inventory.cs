using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int maxSlots;
    public  Dictionary<Item, int> slots { get; private set; } = new Dictionary<Item, int>();

    public Inventory(int maxSlots)
    {
        this.maxSlots = maxSlots;
    }

    public bool AddItem(Item item, int num = 1)
    {
        if (slots.TryGetValue(item, out int count))
        {
            slots[item] = num + count;
            return true;
        }
        if (slots.Count >= maxSlots) return false;
        slots[item] = num;
        return true;
    }
    public bool RemoveItem(Item item, int num = 1)
    {
        if (!slots.TryGetValue(item, out int count)) return false;
        if (count < num) return false;

        if (count == num)
            slots.Remove(item);
        else
            slots[item] = count - num;
        return true;
    }

}
