using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Scriptable Objects/Item/HealItem")]
public class HealItem : ItemBase
{
    public float healAmount;

    private void Reset() => itemType = ItemType.Consumable;

    public override void OnUse(PlayerStateMachine player)
    {
        player.status.Hp += healAmount;
    }
}
