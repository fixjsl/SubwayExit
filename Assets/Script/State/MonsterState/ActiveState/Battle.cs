using MonsterStates;
using System.Threading;
using UnityEngine;

namespace MonsterStates
{
public class Battle : MonsterState
{
    private TimeManager Timer = new TimeManager();
    private float delay;
        public Battle(MonsterStateMachine monster) : base(monster)
    {

    }
    public override void Enter()
    {
            if (Monster.Targetplayer == null)
            {
                Monster.ChangeState<Return>();
                return;
            }
            Monster.animator.CrossFade("battle",0.01f);
            delay = Monster.status.atkdelay + Random.Range(-1, 1);
            Timer.Reset();
        }
    public override void Exit() { 

    }
    public override void LogicUpdate()
    {
            if (Monster.Targetplayer == null)
            {
                Monster.ChangeState<Return>();
                return;
            }
            if (Timer.Timer(delay))
            {
            Monster.ChangeState<Attack>();
             }
            
            if (Vector3.Distance(Monster.Targetplayer.transform.position, Monster.transform.position) > Monster.status.battle_range)
            {
                Monster.ChangeState<Chase>();
            }

    }
    public override void PhysicalUpdate()
    {
        //플레이어와 대치
    }
}
}

