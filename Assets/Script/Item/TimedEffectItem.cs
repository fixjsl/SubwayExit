using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "TimedEffectItem", menuName = "Scriptable Objects/Item/TimedEffectItem")]
public class TimedEffectItem : ItemBase
{
    private void Reset() => itemType = ItemType.Consumable;

    public float duration = 60f;

    [Header("Hunger")]
    public bool modifyHunger;
    public float hungerDecreaseMultiplier = 1f;

    [Header("Thirst")]
    public bool modifyThirst;
    public float thirstDecreaseMultiplier = 1f;

    [Header("Stamina Recovery")]
    public bool modifyStaminaRecovery;
    public float staminaRecoveryMultiplier = 1f;

    public override void OnUse(PlayerStateMachine player)
    {
        if (player.timedEffectCoroutine != null)
            player.StopCoroutine(player.timedEffectCoroutine);
        player.timedEffectCoroutine = player.StartCoroutine(ApplyEffect(player));
    }

    private IEnumerator ApplyEffect(PlayerStateMachine player)
    {
        var status = player.status;

        if (modifyHunger)
            status.hungerDecreasePerMinute = status.baseHungerDecreasePerMinute * hungerDecreaseMultiplier;
        if (modifyThirst)
            status.waterDecreasePerMinute = status.baseWaterDecreasePerMinute * thirstDecreaseMultiplier;
        if (modifyStaminaRecovery)
            status.staminaRecoverey = status.baseStaminaRecovery * staminaRecoveryMultiplier;

        yield return new WaitForSeconds(duration);

        if (modifyHunger)
            status.hungerDecreasePerMinute = status.baseHungerDecreasePerMinute;
        if (modifyThirst)
            status.waterDecreasePerMinute = status.baseWaterDecreasePerMinute;
        if (modifyStaminaRecovery)
            status.staminaRecoverey = status.baseStaminaRecovery;

        player.timedEffectCoroutine = null;
    }
}
