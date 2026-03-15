using UnityEngine;

public class Item : MonoBehaviour 
{
    
    public ItemBase iteminfo { get; private set; }

    public virtual void UseItem()
    { 
    }

    public bool canCraft(Inventory inventory)
    {
        foreach (var material in iteminfo.materials)
        {
            if (!inventory.slots.TryGetValue(material.item, out int count)) return false;
            if (count < material.amount) return false;
        }
        return true;
    }
    public virtual void Craft(Inventory inventory)
    {

    }
}
