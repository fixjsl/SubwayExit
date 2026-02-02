using UnityEngine;

public class Sprint : PlayerState
{
    private float movebuffer;
    private AnimatorStateInfo curAni;
    public Sprint(PlayerStateMachine stateMachine) : base(stateMachine) {
        movebuffer = 1f;
    }
    public override void Enter()
    {
        Debug.Log($"curMovebuffer = {movebuffer.ToString()}");
        player.animator.CrossFade(player.sprint, 0.02f);
    }

    public override void Exit()
    {
        
    }

   


    public override void LogicUpdate()
    {
        if (player.MoveInput != 0 && player.MoveInput != movebuffer)
        {
            canChanged = false;
            player.animator.CrossFade(player.sprintTurn, 0.01f);


            movebuffer = player.MoveInput;
        }

        player.status.Stamina -= player.status.SprintCost * Time.deltaTime;
    }

    public override void PhysicalUpdate()
    {

        if(curAni.shortNameHash != player.sprintTurn)
        {
            if (player.status.currentspeed != player.status.sprintspeed)
            {
            player.status.currentspeed = player.status.sprintspeed;
            }
        ;

        player.Rb.linearVelocity = new Vector3(player.MoveInput * player.status.currentspeed, player.Rb.linearVelocity.y, 0f);
        }
        
    }
    public override void OnAnimationFinished()
    {
        float targetY = (movebuffer < 0) ? -90f : 90f;
        player.transform.rotation = Quaternion.Euler(0, targetY, 0);

        canChanged = true;
        player.animator.CrossFade(player.sprint, 0.01f);
    }
}
