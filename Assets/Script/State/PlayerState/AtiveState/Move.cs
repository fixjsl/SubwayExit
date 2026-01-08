using UnityEngine;

public class Move : State
{

    private float movebuffer;
    private AnimatorStateInfo curAni;
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
        if (Mathf.Abs(player.MoveInput - movebuffer) > 1)
        {
            canChanged = false;
            player.animator.CrossFade(player.moveTurn, 0.02f);
        }

        curAni = player.animator.GetCurrentAnimatorStateInfo(0);

        if(curAni.shortNameHash == player.moveTurn && curAni.normalizedTime >= 1.0f && !player.animator.IsInTransition(0))
        {
            player.animator.CrossFade(player.move, 0.01f);
        }

        movebuffer = player.MoveInput;
    }

    public override void PhysicalUpdate()
    {
        if(player.status.currentspeed != player.status.walkspeed)
        {
            player.status.currentspeed = player.status.walkspeed;
        }
        ;
        
        player.Rb.linearVelocity = new Vector3(player.MoveInput * player.status.currentspeed, player.Rb.linearVelocity.y, 0f);
        
        
    }
}
