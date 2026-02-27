using Unity.Properties;
using UnityEngine;

namespace MonsterStates
{
    public class Hit : MonsterState
    {
        private float hitduration;
        private TimeManager Timer = new TimeManager();
        public Hit(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            Timer.Reset();
            //피격 애니메이션 재생 
            Monster.animator.CrossFade(Monster.hit, 0.01f);
        }
        public override void Exit()
        {

        }

        public void SetHitduration(float duration)
        {
            hitduration = duration;
        }
        public override void LogicUpdate()
        {

        }
        public override void PhysicalUpdate()
        {
            // 무기의 세기에 따라 뒤로 밀려나는 물리량 계산

        }
        public override void OnAnimationFinished()
        {
            if (Timer.Timer(hitduration))
            {
                if(Monster.Targetplayer != null) Monster.ChangeState<Return>();
                else Monster.ChangeState<Battle>();
            }

        }
    }

}

