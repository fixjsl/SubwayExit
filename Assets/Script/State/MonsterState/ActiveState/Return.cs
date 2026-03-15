using MonsterStates;
using UnityEngine;

namespace MonsterStates
{
    public class Return : MonsterState
    {
        private Vector3 spawnPoint;
        private float arrivalThreshold = 0.5f;

        private bool isTurning;
        public Return(MonsterStateMachine monster) : base(monster)
        {
            isTurning = false;
        }
        public override void Enter()
        {



            Monster.status.detection_gauge = 0f;
            Monster.StartDetection();
            this.spawnPoint = Monster.spawnpoint;

            //스폰지점 방향과 현재 몬스터의 얼굴 방향에 따라 회전
            CheckDirection();
        }
        public override void Exit() {
           
        }
        private void CheckDirection()
        {
            

            // 플레이어가 오른쪽인지 왼쪽인지
            float dirToPlayer = Monster.spawnpoint.x - Monster.transform.position.x;
            // 내가 바라보는 방향
            float myDir = Vector3.Dot(Monster.transform.forward, Vector3.right);

            // 반대 방향이면 Turn
            if ((dirToPlayer > 0 && myDir < 0) || (dirToPlayer < 0 && myDir > 0))
            {
                isTurning = true;
                Monster.animator.CrossFade(Monster.moveTurn, 0.01f);
            }
            else
            {
                Monster.animator.CrossFade(Monster.move, 0.01f);
            }
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
            if (!isTurning)
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


 
        }

        public override void OnAnimationFinished()
        {
            isTurning = false;
        }
        public void EndReturn()
        {
            Monster.ChangeState<Idle>();
        }
    }
}

