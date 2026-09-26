using UnityEngine;

public enum PerkAxis
{
    Combat, Gather, Explore
}

[CreateAssetMenu(fileName = "New Perk", menuName = "Perk")]
public class PerkBase : ScriptableObject
{
    public int PerkId;
    public string PerkName;
    public Sprite icon;
    [TextArea(2, 4)] public string description;

    [Header("해금 조건")]
    public PerkAxis axis;
    public int threshold;

    [Header("효과")]
    public int maxHpBonus;
    public int maxStaminaBonus;
    public float carryWeightBonus;
    public ItemBase startingItem;

    public string GetConditionText()
{
    string axisName = axis switch
    {
        PerkAxis.Combat  => "전투",
        PerkAxis.Gather  => "수집",
        PerkAxis.Explore => "탐색",
        _ => ""
    };
    string unit = axis switch
    {
        PerkAxis.Combat  => "마리 처치",
        PerkAxis.Gather  => "개 획득",
        PerkAxis.Explore => "회 귀환",
        _ => ""
    };
    return $"{axisName} {threshold}{unit}";
}

    public void Apply(PlayerStatus status, Inventory inventory)
    {
        status.Maxhp        += maxHpBonus;
        status.MaxStamina   += maxStaminaBonus;
        status.maxCarryWeight += carryWeightBonus;
        if (startingItem != null) inventory.AddItem(startingItem, 1, true);
    }
}