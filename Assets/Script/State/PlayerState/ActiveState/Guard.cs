using UnityEngine;

public class Guard : PlayerState
{



    public Guard(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        isBlock = true;
    }
    public override void Enter()
    {
        //Idle Animation code
        player.animator.CrossFade(player.guard, 0.02f);
    }

    public override void Exit()
    {
        //Stop Idle Animation code
        
    }

    public override void HandleDamage(float Damage)
    {
        player.status.Hp -= (Damage*0.8f);
    }

    
}