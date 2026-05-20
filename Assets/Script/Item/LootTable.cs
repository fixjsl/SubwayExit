using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootEntry
{
    public ItemBase item;
    [Range(0f, 1f)] public float chance = 1f;
    public int minCount = 1;
    public int maxCount = 1;
}

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/LootTable")]
public class LootTable : ScriptableObject
{
    [Header("무조건 드롭")]
    [SerializeField] private List<LootEntry> guaranteedItems;

    [Header("확률 드롭 (각각 독립 계산)")]
    [SerializeField] private List<LootEntry> randomItems;

    public List<(ItemBase item, int count)> Roll()
    {
        var result = new List<(ItemBase, int)>();

        foreach (var entry in guaranteedItems)
        {
            if (entry.item == null) continue;
            result.Add((entry.item, Random.Range(entry.minCount, entry.maxCount + 1)));
        }

        foreach (var entry in randomItems)
        {
            if (entry.item == null) continue;
            if (Random.value <= entry.chance)
                result.Add((entry.item, Random.Range(entry.minCount, entry.maxCount + 1)));
        }

        return result;
    }
}
