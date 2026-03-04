using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    public PlayerStateMachine player;

    private void Awake()
    {
        player = GetComponent<PlayerStateMachine>();
    }
    public void OnAnimationFinished()
    {
        player.ActiveState?.OnAnimationFinished();
    }
    public void OnEndInvincible()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
}
