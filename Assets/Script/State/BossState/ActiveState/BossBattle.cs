using UnityEngine;

namespace BossStates
{
    public class BossBattle : BossState
    {
        private TimeManager attackTimer = new TimeManager();
        private TimeManager strafeTimer = new TimeManager();
        private float delay;
        private float strafeDir;
        private float strafeDuration;

        private float preferredDist => Boss.status.atk_range * 1.3f;
        private const float distThreshold = 0.3f;

        public BossBattle(BossStateMachine boss) : base(boss) { }

        public override void Enter()
        {
            if (Boss.Targetplayer == null)
            {
                Boss.ChangeState<BossReturn>();
                return;
            }

            Boss.Rb.linearVelocity = new Vector3(0f, Boss.Rb.linearVelocity.y, 0f);
            Boss.animator.CrossFade(Boss.sprint, 0.01f);

            if (Boss.PreserveBattleTimer)
            {
                // 피격 복귀: 타이머와 딜레이 유지 (다음 공격까지 남은 시간 그대로)
                Boss.PreserveBattleTimer = false;
            }
            else
            {
                // 일반 진입: 딜레이 새로 설정
                delay = Mathf.Max(0.5f, Boss.status.atkdelay + Random.Range(-1f, 1f));
                attackTimer.Reset();
            }

            strafeDir = (Random.value > 0.5f) ? 1f : -1f;
            strafeDuration = Random.Range(0.8f, 2.0f);
            strafeTimer.Reset();
        }

        public override void Exit() { }

        public override void LogicUpdate()
        {
            if (Boss.Targetplayer == null)
            {
                Boss.ChangeState<BossReturn>();
                return;
            }

            if (!Boss.IsInBattleRange())
            {
                Boss.ChangeState<BossChase>();
                return;
            }

            if (strafeTimer.Timer(strafeDuration))
                PickNewStrafeDir();

            // 이중 체크: 쿨타임 완료 AND 랜덤 발동
            if (attackTimer.Timer(delay))
            {
                if (Boss.IsSpecialReady && Random.value < Boss.SpecialTriggerChance)
                    Boss.ChangeState<BossSpecial>();
                else
                    Boss.ChangeState<BossAttack>();
            }
        }

        public override void PhysicalUpdate()
        {
            if (Boss.Targetplayer == null) return;

            float playerX = Boss.Targetplayer.transform.position.x;
            float myX = Boss.transform.position.x;
            float dist = Mathf.Abs(playerX - myX);

            float targetY = (playerX >= myX) ? 90f : -90f;
            Boss.Rb.rotation = Quaternion.Euler(0f, targetY, 0f);

            float moveX;

            if (dist > preferredDist + distThreshold)
                moveX = (playerX >= myX) ? 1f : -1f;
            else
                moveX = strafeDir;

            Boss.Rb.linearVelocity = new Vector3(moveX * Boss.status.speed, Boss.Rb.linearVelocity.y, 0f);
        }

        private void PickNewStrafeDir()
        {
            strafeDir = -strafeDir;
            strafeDuration = Random.Range(0.8f, 2.0f);
            strafeTimer.Reset();
        }
    }
}
