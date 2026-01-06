using UnityEngine;

public class Move : State
{

    private float movebuffer;
    public Move(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {

        //Move animation
        player.animator.CrossFade(player.move, 0.02f);
    }

    public override void Exit()
    {
        //Stop Idle Animation code

    }
    public override void LogicUpdate()
    {

    }

    public override void PhysicalUpdate()
    {
        if(player.status.currentspeed != player.status.walkspeed)
        {
            player.status.currentspeed = player.status.walkspeed;
        }
        ;
        if (Mathf.Abs(player.MoveInput - movebuffer) > 1)
        {
            player.animator.CrossFade(player.moveTurn, 0.02f);
        }
        else
        {
            player.Rb.linearVelocity = new Vector3(player.MoveInput * player.status.currentspeed, player.Rb.linearVelocity.y, 0f);
        }
        movebuffer = player.MoveInput;
    }
}
