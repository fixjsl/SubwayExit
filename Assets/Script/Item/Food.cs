using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Scriptable Objects/Item/Food")]
public class Food : ItemBase
{
    public int FillingAmount;

    private void Reset() => itemType = ItemType.Consumable;

    public override void OnUse(PlayerStateMachine player)
    {
        player.status.Hungry += FillingAmount;
    }
}