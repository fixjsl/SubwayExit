using UnityEngine;

namespace MonsterStates
{
    public class Stun : MonsterState
    {
        TimeManager stunTimer = new TimeManager();

        
        public Stun(MonsterStateMachine monster) : base(monster) { 

        }
        public override void Enter()
        {
            Monster.Rb.linearVelocity = Vector3.zero;
            Monster.animator.CrossFade(Monster.Stun, 0.01f);
            stunTimer.Reset();
        }

        public override void Exit()
        {
            
        }
        public override void LogicUpdate()
        {
            if (stunTimer.Timer(Monster.status.stunTime))
            {
                if (Monster.Targetplayer != null)
                {
                    Monster.ChangeState<Return>();
                }
                else  Monster.ChangeState<Battle>();
            }
        }

    }
}
