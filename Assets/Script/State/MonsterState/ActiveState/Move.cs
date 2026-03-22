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
            if (Timer.Timer(changeTime))
            {
                Monster.ChangeState<Idle>();
            }
        }
        public override void PhysicalUpdate()
        {
            if (canChanged){
                //3~5������ �����̴� idle���·� ���ư�
                float dot = Vector3.Dot(Monster.transform.forward, Vector3.right);

                // �������� �� ���� ���� ������ 1, �����̸� -1
                moveDir = (dot > 0) ? 1 : -1;

                // 2. ������ moveDir�� �ӵ� �ο�
                Monster.Rb.linearVelocity = new Vector3(moveDir * Monster.status.speed, Monster.Rb.linearVelocity.y, 0f);
            }

        }
        //Trun�ִϸ��̼��϶� ������ ���� ���� �� Move�ִϸ��̼����� ��ȯ
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

