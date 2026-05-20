using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Scriptable Objects/Item/Water")]
public class Water : ItemBase
{
    public int FillingAmount;

    private void Reset() => itemType = ItemType.Consumable;

    public override void OnUse(PlayerStateMachine player)
    {
        player.status.Water += FillingAmount;
    }
}