using UnityEngine;
using UnityEngine.Profiling;

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
        float dot = Vector3.Dot(player.transform.forward, Vector3.right);
        movebuffer = (dot > 0) ? 1f : -1f;
        player.animator.CrossFade(player.move, 0.15f);
    }

    public override void Exit()
    {
        Debug.Log($"curMovebuffer = {movebuffer.ToString()}");
    }
    public override void LogicUpdate()
    {
        if (player.MoveInput != 0 && player.MoveInput != movebuffer)
        {
            float targetY = (movebuffer > 0) ? 90f : -90f;
            player.Rb.rotation = Quaternion.Euler(0, targetY, 0);
            canChanged = false;
            if (player.isSprint)
            {
                player.animator.CrossFade(player.sprintTurn, 0.15f);
            }
            else if (player.isCrunch)
            {
                player.animator.CrossFade(player.crunchTurn, 0.15f);
            }
            else
            {
                player.animator.CrossFade(player.moveTurn, 0.15f);
            }
            


            movebuffer = player.MoveInput;
        }
        curAni = player.animator.GetCurrentAnimatorStateInfo(0);
        if (canChanged)
        {
            if (player.isSprint && curAni.shortNameHash != player.sprint)
                player.animator.CrossFade(player.sprint, 0.15f);
            else if (player.isCrunch && curAni.shortNameHash != player.crunch)
                player.animator.CrossFade(player.crunch, 0.15f);
            else player.animator.CrossFade(player.move, 0.15f);
        }

        if (!player.isSprint)
        {
            player.status.Stamina += player.status.staminaRecoverey * Time.deltaTime;
            
        }
        else
        {
            player.status.Stamina -= player.status.SprintCost * Time.deltaTime;
        }
        if (!canChanged && curAni.shortNameHash == player.moveTurn&& curAni.shortNameHash==player.sprintTurn)
        {
            canChanged = true;
        }
    }

    public override void PhysicalUpdate()
    {
        if (canChanged)
        {
            
            if (player.status.currentspeed != player.status.walkspeed)
                player.status.currentspeed = player.status.walkspeed;
            if (player.isSprint && player.status.currentspeed != player.status.sprintspeed)
                player.status.currentspeed = player.status.sprintspeed;

            player.Rb.linearVelocity = new Vector3(
                player.MoveInput * player.status.currentspeed,
                player.Rb.linearVelocity.y,
                0f);
        }
    }
    public override void OnTurnAnimationFinished()
    {
        float targetY = (player.MoveInput > 0) ? 90f : -90f;
        player.Rb.rotation = Quaternion.Euler(0, targetY, 0);

        if (player.isCrunch)
        {
            player.animator.CrossFade(player.crunch, 0.15f);
        }
        else if (player.isSprint) 
        {
            player.animator.CrossFade(player.sprint, 0.15f);
        }
        else
        {
            player.animator.CrossFade(player.move, 0.15f);
        }
        canChanged = true;
    }
}
