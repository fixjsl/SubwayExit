using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField] protected List<ItemBase> itemPool;
    public int pickCount = 1;
    public int minCount = 1;
    public int maxCount = 1;

    public virtual List<(ItemBase item, int count)> Roll()
    {
        return RollFromPool(itemPool);
    }

    protected List<(ItemBase item, int count)> RollFromPool(List<ItemBase> pool)
    {
        var result = new List<(ItemBase, int)>();
        var copy = new List<ItemBase>(pool);
        int pick = Mathf.Min(pickCount, copy.Count);
        for (int i = 0; i < pick; i++)
        {
            int idx = Random.Range(0, copy.Count);
            result.Add((copy[idx], Random.Range(minCount, maxCount + 1)));
            copy.RemoveAt(idx);
        }
        return result;
    }
}
