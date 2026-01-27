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
        }
        public override void Exit()
        {
            
        }

        public override void LogicUpdate()
        {
            //목표까지의 거리계산
            //목표와의 거리가 일정이상 가까우면 공격
            //콤보가 초기화 된 경우 공격타이머가 전부 지나야 다시 공격 가능 약 2~3초
        }
        public override void PhysicalUpdate()
        {
            //목표까지 이동
        }
    }
}

