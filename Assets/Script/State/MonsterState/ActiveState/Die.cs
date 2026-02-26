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
            //사망애니메이션
            Monster.animator.CrossFade(Monster.die, 0.001f);
            Monster.Rb.linearVelocity = Vector3.zero;
            Monster.enabled = false;
            Monster.GetComponent<Collider>().enabled = false;
        }
        public override void Exit()
        {
            //몬스터 HP회복
            Monster.status.Hp = Monster.status.Maxhp;
        }

        public override void LogicUpdate()
        {
            //몬스터 비활성화 및 몬스터 풀로 되돌림
        }
        public override void PhysicalUpdate()
        {

        }
    }
}

