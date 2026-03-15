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


        // 좌우 배회
        private float strafeDir;       // +1 오른쪽, -1 왼쪽
        private float strafeDuration;  // 이 방향으로 얼마나 걸을지

        // 플레이어와 유지할 적정 거리 (atk_range보다 살짝 바깥)
        private float preferredDist => Monster.status.atk_range * 1.3f;
        private float distThreshold = 0.3f; // 거리 오차 허용 범위
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
            Monster.animator.CrossFade("battle",0.01f);
            delay = Monster.status.atkdelay + Random.Range(-1f, 1f);
            Timer.Reset();

            // 첫 배회 방향 랜덤
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
            
            if (Vector3.Distance(Monster.Targetplayer.transform.position, Monster.transform.position) > Monster.status.battle_range)
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

            //플레이어와 대치
            float playerX = Monster.Targetplayer.transform.position.x;
            float myX = Monster.transform.position.x;
            float dist = Mathf.Abs(playerX - myX);

            // 항상 플레이어 방향 바라보기 (회전만, 뒤돌기 없음)
            float targetY = (playerX >= myX) ? 90f : -90f;
            Monster.Rb.rotation = Quaternion.Euler(0f, targetY, 0f);

            float moveX;

            if (dist < preferredDist - distThreshold)
            {
                // 너무 가까움 → 플레이어 반대 방향으로 슬라이딩 (뒤돌지 않음)
                moveX = (myX >= playerX) ? 1f : -1f;
            }
            else if (dist > preferredDist + distThreshold)
            {
                // 너무 멀어짐 → 플레이어 쪽으로 붙기
                moveX = (playerX >= myX) ? 1f : -1f;
            }
            else
            {
                // 적정 거리 → 좌우 배회
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

