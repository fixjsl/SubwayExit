using UnityEngine;

public class Sprint : State
{
    private float movebuffer;
    private AnimatorStateInfo curAni;
    public Sprint(PlayerStateMachine stateMachine) : base(stateMachine) {
    
    }
    public override void Enter()
    {
        player.animator.CrossFade(player.sprint, 0.02f);
    }

    public override void Exit()
    {
        
    }

   


    public override void LogicUpdate()
    {
        if (Mathf.Abs(player.MoveInput - movebuffer) > 1)
        {
            canChanged = false;
            player.animator.CrossFade(player.sprintTurn, 0.02f);
        }

        curAni = player.animator.GetCurrentAnimatorStateInfo(0);

        if (curAni.shortNameHash == player.sprintTurn && curAni.normalizedTime >= 1.0f && !player.animator.IsInTransition(0))
        {

            player.animator.CrossFade(player.sprint, 0.01f);
        }

        movebuffer = player.MoveInput;

        player.status.Stamina -= player.status.SprintCost * Time.deltaTime;
    }

    public override void PhysicalUpdate()
    {
        if (player.status.currentspeed != player.status.sprintspeed)
        {
            player.status.currentspeed = player.status.sprintspeed;
        }
        ;

        player.Rb.linearVelocity = new Vector3(player.MoveInput * player.status.currentspeed, player.Rb.linearVelocity.y, 0f);
    }
}
