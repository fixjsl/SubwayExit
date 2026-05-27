using Unity.VisualScripting;
using UnityEngine;

public class StairObject : MonoBehaviour
{
    [SerializeField] private Transform stairOffset;
    [SerializeField] private Transform stairBottom;
    [SerializeField] private Transform stairTop;
    [SerializeField] private float dismountYOffset = 0f;
private PlayerStateMachine mountedPlayer;
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

    private void Update()
    {
    if (!IsOnStair || mountedPlayer == null || stairBottom == null || stairTop == null) return;

    float xMin = Mathf.Min(stairBottom.position.x, stairTop.position.x);
    float xMax = Mathf.Max(stairBottom.position.x, stairTop.position.x);
    float t = Mathf.Clamp01(Mathf.InverseLerp(xMin, xMax, mountedPlayer.Rb.position.x));

    float yTarget = stairBottom.position.x < stairTop.position.x
        ? Mathf.Lerp(stairBottom.position.y, stairTop.position.y, t)
        : Mathf.Lerp(stairTop.position.y, stairBottom.position.y, t);

    var pos = mountedPlayer.Rb.position;
    pos.y = yTarget;
    mountedPlayer.Rb.position = pos;
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
        mountedPlayer = player;
        var pos = player.Rb.position;
        
        pos.z = stairOffset.position.z;
        Debug.Log($"Mount : {pos}");
        player.Rb.position = pos;
    }

    private void Dismount(PlayerStateMachine player)
    {
        IsOnStair = false;
        mountedPlayer = null;

        var pos = player.Rb.position;
        pos.z = originalZ;

        if (stairBottom != null && stairTop != null)
        {
            float distToBottom = Mathf.Abs(pos.x - stairBottom.position.x);
            float distToTop = Mathf.Abs(pos.x - stairTop.position.x);
            pos.y = (distToBottom < distToTop ? stairBottom.position.y : stairTop.position.y) + dismountYOffset;
        }

        player.Rb.position = pos;
    }
}
