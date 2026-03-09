using UnityEngine;

public class MonsterAnimationEvent : MonoBehaviour
{




    public MonsterStateMachine monster;

    private void Awake()
    {
        monster = GetComponent<MonsterStateMachine>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnAnimationFinished()
    {
        monster.ActiveState?.OnAnimationFinished();
    }
    public void OnTurnAnimationFinished()
    {
        monster.ActiveState?.OnTurnAnimationFinished();
    }



}
