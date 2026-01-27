using UnityEngine;

public class Move : PlayerState
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
        if ((player.MoveInput - movebuffer >= 1) && player.Rb.rotation.y != -90f || (player.MoveInput - movebuffer) <= -1 && player.Rb.rotation.y != 90f) 
        {
 
            canChanged = false;
            player.animator.CrossFade(player.moveTurn, 0.0001f);
        }

        curAni = player.animator.GetCurrentAnimatorStateInfo(0);

        if(curAni.shortNameHash == player.moveTurn && curAni.normalizedTime >= 1.0f && !player.animator.IsInTransition(0))
        {
            Vector3 currentEuler = player.Rb.rotation.eulerAngles;
            float snappedY = Mathf.Round(currentEuler.y / 90f) * 90f;
            player.Rb.rotation = Quaternion.Euler(0, snappedY, 0);

            canChanged = true;
            player.animator.CrossFade(player.move, 0.0001f);
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

        player.Rb.linearVelocity = new Vector3(player.MoveInput * player.status.currentspeed,0f,0f);
        
        
    }
}
