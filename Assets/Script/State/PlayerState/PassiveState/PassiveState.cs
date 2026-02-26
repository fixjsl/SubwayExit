using System.Collections;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Playables;

public abstract class PassiveState : PlayerState
{
    protected float interval;
    private Coroutine passiveState;

    public PassiveState(PlayerStateMachine player) : base(player)
    {
        
    }
    public override void Enter()
    {
        passiveState = player.StartCoroutine(PassiveRoutine());
    }

    public override void Exit()
    {
        if (passiveState != null)
        {
            player.StopCoroutine(passiveState);
            passiveState = null;
        }
    }

    private IEnumerator PassiveRoutine()
    {
        WaitForSeconds wait = YeildCache.GetIntervals(interval);
        while (true)
        {
            OnTick();
            yield return null;
        }
    }
    protected abstract void OnTick();
}
