using UnityEngine;

public abstract class  State : IState
{
    public PlayerStateMachine player;

    public bool canChanged = true;
    public bool isBlock = false;

    protected State(PlayerStateMachine player)
    {
        this.player = player;
    }

    public abstract void Enter();

    public virtual void LogicUpdate() { }

    public virtual void PhysicalUpdate() { }

    public abstract void Exit();
    public virtual void HandleDamage(float Damage) { }
    public virtual bool CanEnter() => true;

    public virtual void EnableChange() {
        canChanged = true;
    }

}
