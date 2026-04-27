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

    public void OnTurnAnimationFinished()
    {
        player.ActiveState?.OnTurnAnimationFinished();
    }
    public void OncanCombo()
    {
        player.ActiveState?.OncanCombo();
    }
    public void OnAnimationFinishedAt(int attackIndex)
    {
        player.ActiveState?.OnAnimationFinishedAt(attackIndex);
    }
    public void OnAttackCollider() => player.OnAttackColider();
    public void OffAttackCollider() => player.OffAttackColider();
    public void OnEndInvincible()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public void OnParry()
    {
        if(player.ActiveState is Parry)
        {
            ((Parry)player.ActiveState).OnParryWindow();
        }
    }
    public void OnDamage()
    {
        if(player.ActiveState is Execution)
        {
            ((Execution)player.ActiveState).OnDamage();
        }
    }
}
