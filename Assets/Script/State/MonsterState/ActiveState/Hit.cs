using UnityEngine;

namespace MonsterStates
{
    public class Hit : MonsterState
    {
        public Hit(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //피격 애니메이션 재생 
        }
        public override void Exit()
        {

        }

        public override void LogicUpdate()
        {

        }
        public override void PhysicalUpdate()
        {
            // 무기의 세기에 따라 뒤로 밀려나는 물리량 계산

        }
        public override void HandleDamage(float Damage)
        {
            //피격시 함수 
        }
    }

}

