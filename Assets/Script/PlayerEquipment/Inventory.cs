using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int maxSlots;
    private float maxWeight;
    public  Dictionary<int, int> slots { get; private set; } = new Dictionary<int, int>();
    
    public Inventory(int maxSlots)
    {
        this.maxSlots = maxSlots;
    }

    public bool AddItem(Item item, int num = 1)
    {
        if (slots.TryGetValue(item.iteminfo.itemcode, out int count))
        {
            slots[item.iteminfo.itemcode] = num + count;
            return true;
        }
        if (slots.Count >= maxSlots) return false;
        slots[item.iteminfo.itemcode] = num;
        return true;
    }
    public bool RemoveItem(Item item, int num = 1)
    {
        if (!slots.TryGetValue(item.iteminfo.itemcode, out int count)) return false;
        if (count < num) return false;

        if (count == num)
            slots.Remove(item.iteminfo.itemcode);
        else
            slots[item.iteminfo.itemcode] = count - num;
        return true;
    }

}
