using UnityEngine;

public class StairObject : MonoBehaviour
{
    private float stairZ => transform.position.z;

    private float originalZ;
    private StairTrigger mountedFrom;
    public bool IsOnStair { get; private set; }

    public void OnPlayerEnterTrigger(StairTrigger trigger, PlayerStateMachine player)
    {
        if (IsOnStair && trigger != mountedFrom)
        {
            Dismount(player);
            player.ClearInteractable(trigger);
            mountedFrom = null;
        }
    }

    public void TryInteract(StairTrigger trigger, PlayerStateMachine player)
    {
        if (!IsOnStair)
        {
            mountedFrom = trigger;
            Mount(player);
        }
        else
        {
            mountedFrom = null;
            Dismount(player);
        }
    }

    private void Mount(PlayerStateMachine player)
    {
        originalZ = player.Rb.position.z;
        IsOnStair = true;
        var pos = player.Rb.position;
        pos.z = stairZ;
        player.Rb.position = pos;
    }

    private void Dismount(PlayerStateMachine player)
    {
        IsOnStair = false;
        var pos = player.Rb.position;
        pos.z = originalZ;
        player.Rb.position = pos;
    }
}
