
using System.Security;
using UnityEngine;

public class Crunch : PlayerState
{
    private float movebuffer;
    private AnimatorStateInfo curAni;
    public Crunch(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {

        //Move animation
        float dot = Vector3.Dot(player.transform.forward, Vector3.right);
        movebuffer = (dot > 0) ? 1f : -1f;
        player.animator.CrossFade(player.crunch, 0.15f);
    }

    public override void Exit()
    {
        Debug.Log($"curMovebuffer = {movebuffer.ToString()}");
    }
    public override void LogicUpdate()
    {
        curAni = player.animator.GetCurrentAnimatorStateInfo(0);
        if (!player.isCrunch)
        {
            if (curAni.shortNameHash != player.outcrunch && !player.animator.IsInTransition(0) )
            {
                canChanged = false;
                player.animator.CrossFade(player.outcrunch, 0.15f);
            }
            return;
        }
        if (player.MoveInput != 0 && player.MoveInput != movebuffer)
        {
            float targetY = (movebuffer > 0) ? 90f : -90f;
            player.Rb.rotation = Quaternion.Euler(0, targetY, 0);
            canChanged = false;
            player.animator.CrossFade(player.crunchturn, 0.15f);
            movebuffer = player.MoveInput;
        }
        if(player.animator.IsInTransition(0)) return;
        
        if (canChanged)
        {
            if(player.MoveInput != 0)
            {
                if (curAni.shortNameHash != player.crunchmove)
                    player.animator.CrossFade(player.crunchmove, 0.15f,0);
            }
            else if (curAni.shortNameHash != player.crunch)
                player.animator.CrossFade(player.crunch, 0.15f,0);
        }

        if (!player.isSprint)
        {
            player.status.Stamina += player.status.staminaRecoverey * Time.deltaTime; 
        }
        else
        {
            player.status.Stamina -= player.status.SprintCost * Time.deltaTime;
        }
    }

    public override void PhysicalUpdate()
    {
        if (canChanged)
        {

            if (player.status.currentspeed != player.status.crunchspeed)
                player.status.currentspeed = player.status.crunchspeed;
            if (player.isSprint && player.status.currentspeed != player.status.sprintspeed)
                player.status.currentspeed = player.status.sprintspeed - 0.5f;

            player.Rb.linearVelocity = new Vector3(
                player.MoveInput * player.status.currentspeed,
                player.Rb.linearVelocity.y,
                0f);
        }
    }
    public override void OnTurnAnimationFinished()
    {
        float targetY = (movebuffer > 0) ? 90f : -90f;
        player.Rb.rotation = Quaternion.Euler(0, targetY, 0);
        Debug.Log("Turn Animation Finished");
        player.animator.CrossFade(player.crunch, 0.15f);
        canChanged = true;
    }

}
