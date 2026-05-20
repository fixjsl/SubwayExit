using UnityEngine;

public class StairObject : MonoBehaviour
{
    [SerializeField] private Transform stairOffset;

    private float originalZ;
    private StairTrigger mountedFrom;
    public bool IsOnStair { get; private set; }

    public void OnPlayerEnterTrigger(StairTrigger trigger, PlayerStateMachine player)
    {
        if (IsOnStair && trigger != mountedFrom)
        {
            var prev = mountedFrom;
            mountedFrom = null;
            Dismount(player);
            if (prev != null) prev.RefreshPrompt();
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
            var prev = mountedFrom;
            mountedFrom = null;
            Dismount(player);
            if (prev != null) prev.RefreshPrompt();
        }
    }

    private void Mount(PlayerStateMachine player)
    {
        originalZ = player.Rb.position.z;
        IsOnStair = true;
        var pos = player.Rb.position;
        pos.z = stairOffset.position.z;
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
