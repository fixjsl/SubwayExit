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


            isTurning = false;
            Monster.status.detection_gauge = 0f;
            Monster.StartDetection();
            this.spawnPoint = Monster.spawnpoint;

            //�������� ����� ���� ������ �� ���⿡ ���� ȸ��
            CheckDirection();
        }
        public override void Exit() {
           
        }
        private void CheckDirection()
        {
            

            // �÷��̾ ���������� ��������
            float dirToPlayer = Monster.spawnpoint.x - Monster.transform.position.x;
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
                Monster.animator.CrossFade(Monster.move, 0.01f);
            }
        }
        public override void LogicUpdate()
        {
            float distanceToSpawn = Vector3.Distance(Monster.transform.position, spawnPoint);

            // 2. �����ߴٸ� idle ���·� ��ȯ
            if (distanceToSpawn <= arrivalThreshold)
            {
                EndReturn();
            }
        }
        public override void PhysicalUpdate()
        {
            if (!isTurning)
            {
                //�ڱ� ������������ ���ư�
                Vector3 direction = (spawnPoint - Monster.transform.position).normalized;

                // 4. ������ٵ� �ӵ� �ο� (y���� �߷� ������ ���� ���� �� ���)
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

