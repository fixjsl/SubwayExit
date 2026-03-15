using System.Net;
using UnityEngine;

public class Die : PlayerState
{
    public Die(PlayerStateMachine stateMachine) : base(stateMachine) { 
        canChanged = false;
    }
    public override void Enter()
    {
        player.animator.CrossFade(player.die, 0.001f);
        player.Rb.linearVelocity = Vector3.zero;
        player.Rb.isKinematic = true;
        player.gameObject.layer = Layercache.Die;
        player.OnDisable();
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
    public void ContinueGame()
    {
        canChanged = true;
    }
}
