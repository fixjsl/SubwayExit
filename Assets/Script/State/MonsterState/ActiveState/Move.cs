using UnityEngine;

namespace MonsterStates
{
    public class move : MonsterState
    {
        public move(MonsterStateMachine monster) : base(monster)
        {
            
        }
        public override void Enter()
        {
            canChanged = false;
        }
        public override void Exit()
        {

        }

        public override void LogicUpdate()
        {
            //플레이어 탐색 함수 주기적 호출
            //타이머는 상태머신에 있는 공용함수로 사용
        }
        public override void PhysicalUpdate()
        {
            //3~5초정도 움직이다 idle상태로 돌아감
            
        }
    }
}

