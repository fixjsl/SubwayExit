using MonsterStates;
using UnityEngine;

namespace MonsterStates
{
    public class Idle : MonsterState
    {
        private TimeManager Timer = new TimeManager();
        private float changeTime;
        public Idle(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //idle애니메이션 재생
            changeTime = Random.Range(3, 5);
            Monster.StartDetection();
            Timer.Reset();
        }
        public override void Exit()
        {

        }

        public override void LogicUpdate()
        {
            if (Monster.status.detection_gauge >= 1f)
            {
                Monster.ChangeState<Chase>();
            }
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

