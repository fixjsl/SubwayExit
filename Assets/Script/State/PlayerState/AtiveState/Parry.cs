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

    public override void HandleUpdate()
    {

    }

    public override void LogicUpdate()
    {
        // input Logic
        
            parryTimer -= Time.deltaTime;   
            if (parryTimer <=0)

            {
            parryTimer = 0;
            if (player.isGuard)
            {
                ChangeState<Guard>();
            }
            else if (player.ConsumeBuffer(StateType.Attack))
            {
                ChangeState<Attack>();
            }
            else if (player.ConsumeBuffer(StateType.Dodge))
            {
                ChangeState<Dodge>();
            }
            else
            {
                ChangeState<Idle>();
            }
            }
        


    }

    public override void PsycialUpdate()
    {


    }
}