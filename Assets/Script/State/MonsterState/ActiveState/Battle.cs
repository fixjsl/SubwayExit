using MonsterStates;
using System.Threading;
using UnityEngine;

namespace MonsterStates
{
public class Battle : MonsterState
{
    private TimeManager Timer = new TimeManager();
        private TimeManager strafeTimer = new TimeManager();
        private float delay;


        //¿ ȸ
        private float strafeDir;       // +1, -1
        private float strafeDuration;  //  󸶳

        // �÷��̾�� ������ ���� �Ÿ� (atk_range���� ��¦ �ٱ�)
        private float preferredDist => Monster.status.atk_range * 1.3f;
        private float distThreshold = 0.3f; // �Ÿ� ���� ��� ����
        public Battle(MonsterStateMachine monster) : base(monster)
    {

    }
    public override void Enter()
    {
            if (Monster.Targetplayer == null)
            {
                Monster.ChangeState<Return>();
                return;
            }
            Monster.Rb.linearVelocity = new Vector3(0f, Monster.Rb.linearVelocity.y, 0f);
            Monster.animator.CrossFade(Monster.sprint,0.01f);
            delay = Mathf.Max(0.5f, Monster.status.atkdelay + Random.Range(-1f, 1f));
            Timer.Reset();

            // ù ��ȸ ���� ����
            strafeDir = (Random.value > 0.5f) ? 1f : -1f;
            strafeDuration = Random.Range(0.8f, 2.0f);
            strafeTimer.Reset();
        }
    public override void Exit() { 

    }
    public override void LogicUpdate()
    {
            if (Monster.Targetplayer == null)
            {
                Monster.ChangeState<Return>();
                return;
            }
            if (Timer.Timer(delay))
            {
            Monster.ChangeState<Attack>();
             }
            
            if (!Monster.IsInBattleRange())
            {
                Monster.ChangeState<Chase>();
            }
            if (strafeTimer.Timer(strafeDuration))
            {
                PickNewStrafeDir();
            }
        }
    public override void PhysicalUpdate()
    {
            if (Monster.Targetplayer == null) return;

            //�÷��̾�� ��ġ
            float playerX = Monster.Targetplayer.transform.position.x;
            float myX = Monster.transform.position.x;
            float dist = Mathf.Abs(playerX - myX);

            // �׻� �÷��̾� ���� �ٶ󺸱� (ȸ����, �ڵ��� ����)
            float targetY = (playerX >= myX) ? 90f : -90f;
            Monster.Rb.rotation = Quaternion.Euler(0f, targetY, 0f);

            float moveX;

            if (dist > preferredDist + distThreshold)
            {
                // �ʹ� �־��� �� �÷��̾� ������ �ٱ�
                moveX = (playerX >= myX) ? 1f : -1f;
            }
            else
            {
                // ���� �Ÿ� �� �¿� ��ȸ
                moveX = strafeDir;
            }

            Monster.Rb.linearVelocity = new Vector3(moveX * Monster.status.speed, Monster.Rb.linearVelocity.y,0f);
        }
        private void PickNewStrafeDir()
        {
            strafeDir = -strafeDir;
            strafeDuration = Random.Range(0.8f, 2.0f);
            strafeTimer.Reset();
        }
    }
}

