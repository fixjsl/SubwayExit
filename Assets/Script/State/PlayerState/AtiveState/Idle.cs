using UnityEngine;

public class Idle : State
{



    public Idle(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
         //speed init
        player.Rb.linearVelocity = Vector3.zero;
        //Idle Animation code
        player.animator.CrossFade(player.idle, 0.2f);

    }

    public override void Exit()
    {
        //Stop Idle Animation code

    }



    public override void LogicUpdate()
    {
        // input Logic

    }

    public override void PhysicalUpdate()
    {


    }
}
