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
            Monster.animator.speed = Monster.HitAnimLength / Mathf.Max(hitduration, 0.01f);
            Monster.animator.CrossFade(Monster.hit, 0.01f);
        }
        public override void Exit()
        {
            Monster.animator.speed = 1f;
        }

        public void SetHitduration(float duration)
        {
            hitduration = duration;
        }
        public override void LogicUpdate()
        {
            if (Timer.Timer(hitduration))
            {
                if (Monster.Targetplayer != null) Monster.ChangeState<Battle>();
                else Monster.ChangeState<Return>();
            }
        }
        public override void PhysicalUpdate()
        {
            // ������ ���⿡ ���� �ڷ� �з����� ������ ���

        }
        public override void OnAnimationFinished()
        {

        }
    }

}

