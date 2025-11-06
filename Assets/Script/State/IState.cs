using UnityEditor;
using UnityEngine;

public interface IState
{
    public void Enter();

    public void HandleUpdate();
    public void LogicUpdate();
    public void PsycialUpdate();

    public void Exit();
}
