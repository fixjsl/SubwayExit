using System.Threading;
using UnityEngine;

public class Parry: PlayerState
{
    float parryTimer;

    public Parry(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        isBlock = true;
        canChanged = false;
    }
    public override void Enter()
    {
        //Guard Animation code


        parryTimer = player.status.parryduration;
        player.animator.CrossFade(player.parrying, 0.02f);
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
            parryTimer -= Time.deltaTime;   
            if (parryTimer <=0)
            {
            parryTimer = 0;
            canChanged = true;
            }
    }

    public override void PhysicalUpdate()
    {


    }
}