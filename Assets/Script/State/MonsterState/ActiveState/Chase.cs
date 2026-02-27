using UnityEngine;


namespace MonsterStates
{
    public class Chase : MonsterState
    {
        private bool isTurning = false;
        public Chase(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //추격 애니메이션
            Monster.StopDetection();
            isTurning = false;
            CheckDirection();
        }
        public override void Exit()
        {
            
        }
        private void CheckDirection()
        {
            if (Monster.Targetplayer == null) return;

            // 플레이어가 오른쪽인지 왼쪽인지
            float dirToPlayer = Monster.Targetplayer.transform.position.x - Monster.transform.position.x;
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
                Monster.animator.CrossFade(Monster.sprint, 0.01f);
            }
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
            if (Monster.Targetplayer == null) return;
            
                // 플레이어 방향으로 회전
            CheckDirection();

            if (isTurning) return;
                // 플레이어 방향으로 이동 로직
            Vector3 direction = (Monster.Targetplayer.transform.position - Monster.transform.position).normalized;
            Monster.Rb.linearVelocity = direction * Monster.status.speed;
            
        }
        public override void OnAnimationFinished()
        {
            Vector3 currentEuler = Monster.Rb.rotation.eulerAngles;
            float snappedY = Mathf.Round(currentEuler.y / 90f) * 90f;
            Monster.Rb.rotation = Quaternion.Euler(0, snappedY, 0);

            canChanged = true;
            Monster.animator.CrossFade(Monster.move, 0.0001f);
        }
    }
}

