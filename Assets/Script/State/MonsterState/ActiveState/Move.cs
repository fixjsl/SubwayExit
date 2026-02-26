using UnityEngine;

namespace MonsterStates
{
    public class move : MonsterState
    {
        private TimeManager Timer = new TimeManager();
        private float changeTime;
        private float movebuffer;
        private float moveDir;
        public move(MonsterStateMachine monster) : base(monster)
        {
            
        }
        public override void Enter()
        {
            changeTime = Random.Range(3f, 5f);
            Monster.animator.CrossFade(Monster.moveTurn, 0.01f);
            canChanged = false;
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
            if (Timer.Timer(changeTime))
            {
                Monster.ChangeState<Idle>();
            }
        }
        public override void PhysicalUpdate()
        {
            if (!Monster.animator.GetCurrentAnimatorStateInfo(0).IsName("moveTurn")){
                //3~5초정도 움직이다 idle상태로 돌아감
                float dot = Vector3.Dot(Monster.transform.forward, Vector3.right);

                // 오른쪽을 더 많이 보고 있으면 1, 왼쪽이면 -1
                moveDir = (dot > 0) ? 1 : -1;

                // 2. 결정된 moveDir로 속도 부여
                Monster.Rb.linearVelocity = new Vector3(moveDir * Monster.status.speed, Monster.Rb.linearVelocity.y, 0f);
            }

        }
        //Trun애니메이션일때 끝나면 방향 보정 후 Move애니메이션으로 전환
        public  override void OnAnimationFinished()
        {
            Vector3 currentEuler = Monster.Rb.rotation.eulerAngles;
            float snappedY = Mathf.Round(currentEuler.y / 90f) * 90f;
            Monster.Rb.rotation = Quaternion.Euler(0, snappedY, 0);

            canChanged = true;
            Monster.animator.CrossFade(Monster.move, 0.0001f);
        }
    }
}

