using UnityEngine;

namespace MonsterStates
{
    public class Attack : MonsterState
    {
        private int comboindex;
        private bool isAnimationFinished;
        public Attack(MonsterStateMachine monster) : base(monster)
        {
        }
        public override void Enter()
        {
            //공격 애니메이션
            isAnimationFinished = false;
            Monster.animator.CrossFade(Monster.attackHashes[comboindex], 0.01f);

        }
        public override void Exit()
        {
            //애니메이션이 끝나지 않았을떄 상태전환이 되면
            if (!isAnimationFinished)
            {
                comboindex = 0;
            }

        }

        public override void LogicUpdate()
        {

        }
        public override void PhysicalUpdate()
        {

        }

        public void OnAttackColider()
        {
            //공격 콜라인더 키기
            if(Monster.AttackCollider != null)
            {
                Monster.AttackCollider.enabled = true;
            }
        }
        public void OffAttackColider()
        {
            //공격 콜라인더 끄기
            if(Monster.AttackCollider != null)
            {
                Monster.AttackCollider.enabled = false;
            }
        }
        public override void OnAnimationFinished()
        {
            isAnimationFinished = true;
            if (comboindex < 2)
            {
                comboindex++;
                Monster.ChangeState<Attack>();
            }
            else if (comboindex == 2)
            {
                comboindex = 0;
                Monster.ChangeState<Battle>();
            }
        }

    }

    }

