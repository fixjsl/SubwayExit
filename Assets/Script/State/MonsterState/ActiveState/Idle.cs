using MonsterStates;
using UnityEngine;

namespace MonsterStates
{
    public class idle : MonsterState
    {
        private TimeManager Timer = new TimeManager();
        private float changeTime;
        public idle(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //idle애니메이션 재생
            changeTime = Random.Range(3, 5);
        }
        public override void Exit()
        {

        }

        public override void LogicUpdate()
        {
            //탐색중 함수 실행
            //3~5초뒤 Move로 전환
            if (Timer.Timer(changeTime))
            {
                Monster.ChangeState<move>();
            }
        }
        public override void PhysicalUpdate()
        {
            
        }
    }
}

