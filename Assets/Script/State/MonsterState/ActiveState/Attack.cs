using UnityEngine;

namespace MonsterStates
{
    public class Attack : MonsterState
    {
        public Attack(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //공격 애니메이션

        }
        public override void Exit()
        {
            //콤보 0
        }

        public override void LogicUpdate()
        {
            //애니메이션 종료 이벤트가 오면 다시  Attack로
            //아니면 공격 타이머 시작 후 Chanse로 전환
        }
        public override void PhysicalUpdate()
        {
            //애니메이션의 정도의 따라 이벤트를 받고 앞으로 물리량 주기
        }
    }

}

