using UnityEngine;

public class Die : State
{
    public Die(PlayerStateMachine stateMachine) : base(stateMachine) { 
        canChanged = false;
    }
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }





    public override void LogicUpdate()
    {
        //비활성화
    }

    public override void PhysicalUpdate()
    {
        //비활성화
    }
}
