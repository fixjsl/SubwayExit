using UnityEngine;

namespace MonsterStates{
public abstract class MonsterState : State
{

    protected MonsterStateMachine Monster;
    protected bool HasTarget => Monster.Targetplayer != null;
    protected MonsterState(MonsterStateMachine monster)
    {
        Monster = monster;      
    }

    public bool ScanPlayer()
    {
            //플레이어 추적 함수 추적 범위에 플레이어가 있으면 true를 반환
            return true;
    }


    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void HandleDamage(float Damage)
    {
            //슈퍼아머용 데미지 함수
            Monster.status.Hp -= Damage;

        }
        public virtual void OnAnimationFinished() {}
    }


}

