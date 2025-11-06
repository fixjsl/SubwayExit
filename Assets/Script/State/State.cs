using UnityEditor;
using UnityEngine;

public abstract class  State : IState
{
    PlayerStateMachine player;

    protected State(PlayerStateMachine player)
    {
        this.player = player;
    }

    public abstract void Enter();

    public abstract void HandleUpdate();

    public abstract void LogicUpdate();

    public abstract void PsycialUpdate();

    public abstract void Exit();
}
