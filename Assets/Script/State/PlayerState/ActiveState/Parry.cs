using System.Threading;
using UnityEngine;

public class Parry: PlayerState
{
    private TimeManager parryTimer = new TimeManager();

    public bool IsInParryWindow { get; private set; }

    public Parry(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        isBlock = true;
    }
    public override void Enter()
    {
        canChanged = false;
        IsInParryWindow = true;
        parryTimer.Reset();
        player.animator.CrossFade(player.parrying, 0.02f);
        player.status.UseStamina(player.currentWeapon.status.parryStamina);
    }

    public override void Exit()
    {

    }

    public override void HandleDamage(float Damage)
    {
        
    }

    public override void LogicUpdate()
    {
        // input Logic
        if (parryTimer.Timer(player.status.parryduration))
        {
            IsInParryWindow = false;
            canChanged = true;
        }
    }

    public override void PhysicalUpdate()
    {


    }
}