using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Scriptable Objects/Item/Food")]
public class Food : ItemBase
{
    public int FillingAmount;

    private void Reset() => itemType = ItemType.Consumable;

    public override bool OnUse(PlayerStateMachine player)
    {
        player.status.Hungry += FillingAmount;
        return true;
    }

    public override string GetEffectDescription() => $"배고픔 +{FillingAmount}";
}