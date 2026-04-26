using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Weapon, Consumable, Material, Equipment }

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    public Sprite icon;
    public int itemcode;
    public ItemType itemType;
    public int maxamount;
    public int weight;

    public virtual void OnUse(PlayerStateMachine player) { }
    [System.Serializable]
    public struct CraftMaterial
    {
        public Item item;
        public int amount;
    }
    public List<CraftMaterial> materials;
}
