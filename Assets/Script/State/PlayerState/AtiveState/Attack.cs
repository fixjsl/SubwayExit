using UnityEngine;

public class Attack : State
{



    public Attack(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        canChanged = false;
    }
    public override void Enter()
    {
        //Idle Animation code
        canChanged = true;
    }

    public override void Exit()
    {
        //Stop Idle Animation code
        
    }
 
    public override void LogicUpdate()
    {
        // input Logic
        
    }

    public override void PhysicalUpdate()
    {

        
    }
}
