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
            //���� �ִϸ��̼�
            isAnimationFinished = false;
            Monster.animator.CrossFade(Monster.attackHashes[comboindex], 0.01f);

        }
        public override void Exit()
        {
            //�ִϸ��̼��� ������ �ʾ����� ������ȯ�� �Ǹ�
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

