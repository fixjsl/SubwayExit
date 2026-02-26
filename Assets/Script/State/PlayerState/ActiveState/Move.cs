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
            player.animator.CrossFade(player.moveTurn, 0.15f);


            movebuffer = player.MoveInput;
        }
        if (!canChanged && !player.animator.GetCurrentAnimatorStateInfo(0).IsName("moveTurn"))
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

            player.Rb.linearVelocity = new Vector3(
                player.MoveInput * player.status.currentspeed,
                player.Rb.linearVelocity.y,
                0f);
        }
    }
    public override void OnAnimationFinished()
    {
        float targetY = (player.MoveInput > 0) ? 90f : -90f;
        player.Rb.rotation = Quaternion.Euler(0, targetY, 0);

        canChanged = true;
        player.animator.CrossFade(player.move, 0.15f);
    }
}
