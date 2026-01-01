using UnityEngine;

public abstract class  State : IState
{
    public PlayerStateMachine player;

    protected State(PlayerStateMachine player)
    {
        this.player = player;
    }

    public abstract void Enter();

    public abstract void HandleUpdate();

    public abstract void LogicUpdate();

    public abstract void PsycialUpdate();

    public abstract void Exit();

    protected void ChangeState<T>() where T : State
    {
        player.ChangeState<T>();
    }
}
