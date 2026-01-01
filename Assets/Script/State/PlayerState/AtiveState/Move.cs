using UnityEngine;

public class Move : State
{
    public Move(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {

        //Move animation
    }

    public override void Exit()
    {
        //Stop Idle Animation code

    }

    public override void HandleUpdate()
    {
        
    }

    public override void LogicUpdate()
    {
        // input Logic
        if(player.MoveInput == 0f)
        {
            ChangeState<Idle>();
        }
    }

    public override void PsycialUpdate()
    {
        float speed = player.isSprint ? player.status.sprintspeed : player.status.speed;

        player.Rb.linearVelocity = new Vector3(player.MoveInput * speed, player.Rb.linearVelocity.y, 0f);
    }
}
