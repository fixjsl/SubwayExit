using UnityEngine;

public class Interect : PlayerState
{

    private Object interectObject;
    public Interect(PlayerStateMachine stateMachine) : base(stateMachine) {
    
    }
    public override void Enter()
    {
        //감지한 물체가 없다면 바로 exit;
        player.nearbyInteractable?.Oninterect(player.transform.position);
        //감지한 물체가 있다면 해당 스크립트로 이동
    }

    public override void Exit()
    {
        
    }




    public override void LogicUpdate()
    {
        
    }

    public override void PhysicalUpdate()
    {
        
    }
}
