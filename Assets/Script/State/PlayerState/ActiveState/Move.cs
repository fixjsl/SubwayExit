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
        Debug.Log($"curMovebuffer = {movebuffer.ToString()}");
    }
    public override void LogicUpdate()
    {
        if (player.MoveInput != 0 && player.MoveInput != movebuffer)
        {
            canChanged = false; 
            player.animator.CrossFade(player.moveTurn, 0.01f);


            movebuffer = player.MoveInput;
        }
    }

    public override void PhysicalUpdate()
    {
        if(curAni.shortNameHash != player.moveTurn)
        {
            if(player.status.currentspeed != player.status.walkspeed)
            {
            player.status.currentspeed = player.status.walkspeed;
            }
        ;

            player.Rb.linearVelocity = new Vector3(player.MoveInput * player.status.currentspeed,0f,0f);
        }        
    }
    public override void OnAnimationFinished()
    {
        Vector3 currentEuler = player.Rb.rotation.eulerAngles;
        float snappedY = Mathf.Round(currentEuler.y / 90f) * 90f;
        player.Rb.rotation = Quaternion.Euler(0, snappedY, 0);

        canChanged = true;
        player.animator.CrossFade(player.move, 0.0001f);
    }
}
