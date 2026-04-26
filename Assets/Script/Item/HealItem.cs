using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Scriptable Objects/Item/HealItem")]
public class HealItem : ItemBase
{
    public float healAmount;

    public override void OnUse(PlayerStateMachine player)
    {
        player.status.Hp += healAmount;
    }
}
