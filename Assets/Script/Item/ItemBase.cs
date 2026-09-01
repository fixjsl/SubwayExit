using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Consumable, Material, Equipment, InterectObject, KeyItem }

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    public Sprite icon;
    public Sprite firstAcquireImage;
    public int itemcode;
    public ItemType itemType;
    [TextArea(2, 4)] public string description;
    public int maxamount;
    public float weight;
    public bool canBurnAsFuel;

    public virtual bool OnUse(PlayerStateMachine player) { return true; }
    public virtual string GetEffectDescription() { return ""; }
    [System.Serializable]
    public struct CraftMaterial
    {
        public ItemBase item;
        public int amount;
    }
    public List<CraftMaterial> materials;

    public bool CanCraft(Inventory inventory)
    {
        foreach (var material in materials)
        {
            if (!inventory.slots.TryGetValue(material.item.itemcode, out int count)) return false;
            if (count < material.amount) return false;
        }
        return true;
    }

    public void Craft(Inventory inventory)
    {
        if (!CanCraft(inventory)) return;
        foreach (var material in materials)
            inventory.RemoveItem(material.item, material.amount);
        inventory.AddItem(this);
    }
}
