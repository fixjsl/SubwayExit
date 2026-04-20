using UnityEngine;

public abstract class PlayerState : State
{
    protected PlayerStateMachine player;

    protected PlayerState(PlayerStateMachine player) 
    {
        this.player = player;
    }

    public override  void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }
    public virtual void OnAnimationFinished()
    {
        Debug.Log($"[PlayerState] OnAnimationFinished - State: {GetType().Name}");
        canChanged = true;
    }
    public virtual void OnTurnAnimationFinished() { }
    public virtual void OncanCombo() { }
    public virtual void OnAnimationFinishedAt(int attackIndex) { }

}
