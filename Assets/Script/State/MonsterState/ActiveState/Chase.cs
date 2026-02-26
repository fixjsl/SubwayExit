using UnityEngine;


namespace MonsterStates
{
    public class Chase : MonsterState
    {
        public Chase(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //추격 애니메이션
            Monster.StopDetection();
        }
        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            //목표까지의 거리계산
            //목표와의 거리가 일정이상 가까우면 공격
            if (Vector3.Distance(Monster.Targetplayer.transform.position, Monster.transform.position) < Monster.status.atk_range)
            {
                Monster.ChangeState<Attack>();
            }
            
        }
        public override void PhysicalUpdate()
        {
            //목표까지 이동
            if (Monster.Targetplayer != null)
            {
                // 플레이어 방향으로 이동 로직
                Vector3 direction = (Monster.Targetplayer.transform.position - Monster.transform.position).normalized;
                Monster.Rb.linearVelocity = direction * Monster.status.speed;
            }
        }
    }
}

