using UnityEngine;


namespace MonsterStates
{
    public class Die : MonsterState
    {
        public Die(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //����ִϸ��̼�
            Monster.animator.CrossFade(Monster.die, 0.001f);
            Monster.Rb.linearVelocity = Vector3.zero;
            Monster.enabled = false;
            Monster.GetComponent<Collider>().enabled = true;
        }
        public override void Exit()
        {
            //���� HPȸ��
            Monster.status.Hp = Monster.status.Maxhp;
        }

        public override void LogicUpdate()
        {
            //���� ��Ȱ��ȭ �� ���� Ǯ�� �ǵ���
        }
        public override void PhysicalUpdate()
        {

        }
    }
}

