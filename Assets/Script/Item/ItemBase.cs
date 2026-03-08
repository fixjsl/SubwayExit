using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemBase", menuName = "Scriptable Objects/ItemBase")]
public class ItemBase : ScriptableObject
{
    public Sprite icon;
    public int itemcode;
    public int maxamount; // 최대 수량
    public int weight; //무게 (이거 쓰냐)
    [System.Serializable]
    public struct CraftMaterial
    {
        public Item item;
        public int amount;
    }
    public List<CraftMaterial> materials;
}
