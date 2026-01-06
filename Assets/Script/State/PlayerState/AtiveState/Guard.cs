using UnityEngine;

public class Guard : State
{



    public Guard(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void Enter()
    {
        //Idle Animation code
        
    }

    public override void Exit()
    {
        //Stop Idle Animation code
        
    }

    public override void HandleDamage(float Damage)
    {
        
    }

    


    public override void LogicUpdate()
    {
        // input Logic
        
    }

    public override void PhysicalUpdate()
    {

        
    }
}