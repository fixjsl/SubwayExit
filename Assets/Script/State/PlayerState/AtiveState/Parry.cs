using System.Threading;
using UnityEngine;

public class Parry: State
{
    float parryTimer;

    public Parry(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        //Guard Animation code


        parryTimer = player.status.parryduration;
    }

    public override void Exit()
    {
        //Stop Idle Animation code
        if (!player.isGuard)
        {
            //stop animation
        }

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