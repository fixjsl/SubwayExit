
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
        canChanged = false;
        
        player.animator.CrossFade(player.incrunch, 0.15f);
    }

    public override void Exit()
    {
        Debug.Log($"curMovebuffer = {movebuffer.ToString()}");
    }
    public override void LogicUpdate()
    {
        if (!player.isCrunch)
        {
            if (curAni.shortNameHash != player.outcrunch)
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
            player.animator.CrossFade(player.crunchTurn, 0.15f);
            movebuffer = player.MoveInput;
        }

        if (player.animator.IsInTransition(0)) return;

        curAni = player.animator.GetCurrentAnimatorStateInfo(0);
    
        if (player.MoveInput != 0)
            player.animator.CrossFade(player.crunchMove, 0.15f);  // 해시 추가 필요
        else
            player.animator.CrossFade(player.crunchIdle, 0.15f);
        if (!canChanged && (curAni.shortNameHash == player.crunchTurn))
        {
            canChanged = true;
        }
    }

    public override void PhysicalUpdate()
    {
        if (canChanged)
        {

            if (player.status.currentspeed != player.status.crunchspeed)
                player.status.currentspeed = player.status.crunchspeed;

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

        player.animator.CrossFade(player.crunchIdle, 0.15f);
        canChanged = true;
    }

}
