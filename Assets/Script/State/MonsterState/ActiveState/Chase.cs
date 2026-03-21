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
            //�߰� �ִϸ��̼�
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

            // �÷��̾ ���������� ��������
            float dirToPlayer = Monster.Targetplayer.transform.position.x - Monster.transform.position.x;
            // ���� �ٶ󺸴� ����
            float myDir = Vector3.Dot(Monster.transform.forward, Vector3.right);

            // �ݴ� �����̸� Turn
            if ((dirToPlayer > 0 && myDir < 0) || (dirToPlayer < 0 && myDir > 0))
            {
                isTurning = true;
                Monster.animator.CrossFade(Monster.moveTurn, 0.01f);
            }
            else
            {
                isTurning = false;
                Monster.animator.CrossFade(Monster.sprint, 0.01f);
            }
        }
        public override void LogicUpdate()
        {
            //��ǥ������ �Ÿ����
            //��ǥ���� �Ÿ��� �����̻� ������ ����
            if(Monster.Targetplayer == null)
            {
                Monster.ChangeState<Return>();
                return;
            }
            if (Vector3.Distance(Monster.Targetplayer.transform.position, Monster.transform.position) < Monster.status.atk_range)
            {
                Monster.ChangeState<Attack>();
            }
            
        }
        public override void PhysicalUpdate()
        {
            //��ǥ���� �̵�
            if (Monster.Targetplayer == null) return;

            AnimatorStateInfo stateInfo = Monster.animator.GetCurrentAnimatorStateInfo(0);
            // �÷��̾� �������� ȸ��
            if (stateInfo.shortNameHash != Monster.moveTurn && !Monster.animator.IsInTransition(0))
            {
                CheckDirection();
            }
           

            if (isTurning) return;
                // �÷��̾� �������� �̵� ����
            Vector3 direction = (Monster.Targetplayer.transform.position - Monster.transform.position).normalized;
            Monster.Rb.linearVelocity = direction * Monster.status.speed;
            
        }
        public override void OnTurnAnimationFinished()
        {
            Vector3 currentEuler = Monster.Rb.rotation.eulerAngles;
            float snappedY = Mathf.Round(currentEuler.y / 90f) * 90f;
            Monster.Rb.rotation = Quaternion.Euler(0, snappedY, 0);

            isTurning = false;

            Monster.animator.CrossFade(Monster.sprint, 0.0001f);
        }
    }
}

