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
        //여기도 뭐 없자나 나중에 여유되면 애니메이션 추가로 좀 넣죠

    }

    public override void PhysicalUpdate()
    {


    }
}
