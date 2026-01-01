using UnityEngine;

public interface StateMachine
{
    public void stateInit();
    public void ChangeState<T>() where T : State;
}
