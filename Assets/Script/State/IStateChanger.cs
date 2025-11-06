using UnityEngine;

public interface StateMachine
{
    public void stateInit();
    public void ChangeState(State state1);
}
