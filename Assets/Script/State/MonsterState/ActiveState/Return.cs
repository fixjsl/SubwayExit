using MonsterStates;
using UnityEngine;

namespace MonsterStates
{
    public class Return : MonsterState
    {
        private Vector3 spawnPoint;
        private float arrivalThreshold = 0.5f;
        public Return(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {



            Monster.status.detection_gauge = 0f;
            Monster.StartDetection();
            this.spawnPoint = Monster.spawnpoint;

            //스폰지점 방향과 현재 몬스터의 얼굴 방향에 따라 회전
        }
        public override void Exit() {
           
        }
        public override void LogicUpdate()
        {
            float distanceToSpawn = Vector3.Distance(Monster.transform.position, spawnPoint);

            // 2. 도착했다면 idle 상태로 전환
            if (distanceToSpawn <= arrivalThreshold)
            {
                EndReturn();
            }
        }
        public override void PhysicalUpdate()
        {
            //자기 스폰지점으로 돌아감
            Vector3 direction = (spawnPoint - Monster.transform.position).normalized;

            // 4. 리지드바디에 속도 부여 (y축은 중력 유지를 위해 기존 값 사용)
            Monster.Rb.linearVelocity = new Vector3(
                direction.x * Monster.status.speed,
                Monster.Rb.linearVelocity.y,
                0f
            );

 
        }
        
        public void EndReturn()
        {
            Monster.ChangeState<Idle>();
        }
    }
}

