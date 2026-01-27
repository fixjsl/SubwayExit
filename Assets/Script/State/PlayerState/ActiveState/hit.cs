using UnityEngine;

public class hit : PlayerState
{



    public hit(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        canChanged = false;
    }
    public override void Enter()
    {
        //Idle Animation code
        player.animator.CrossFade(player.hit, 0.02f);
    }

    public override void Exit()
    {

    }

    public override void HandleDamage(float Damage)
    {
        player.status.Hp = player.status.Hp - Damage;
    }

    


    public override void LogicUpdate()
    {
        // input Logic

    }

    public override void PhysicalUpdate()
    {


    }
}