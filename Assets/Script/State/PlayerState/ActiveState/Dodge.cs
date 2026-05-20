
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
public class Dodge : PlayerState
{
    private float cooltime;
    private float lastTime;
    
    private Vector3 DodgeRange = new Vector3 (30,0,0);

    public Dodge(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        cooltime = player.status.DodgeCooldown;
        lastTime = -99f;
        isBlock = true;
    }

    public override bool CanEnter()
    {
        if (player.isTired) return false;
        return Time.time >= lastTime + cooltime && player.status.Stamina >= player.status.DodgeCost;
    }
    public override void Enter()
    {

        lastTime = Time.time;
        canChanged = false;
        player.gameObject.layer = LayerMask.NameToLayer("Dodge");
        player.status.UseStamina(player.status.DodgeCost);
        player.animator.applyRootMotion = true;
        player.Rb.isKinematic = false;
        player.animator.SetLayerWeight(1, 0f);
        player.animator.CrossFade(player.dodge, 0.15f);

    }

    public override void Exit()
    {
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.animator.applyRootMotion = false;
        player.animator.SetLayerWeight(1, 1f);
        player.Rb.linearVelocity = new Vector3(0f, player.Rb.linearVelocity.y, 0f);
    }


    public override void LogicUpdate()
    {
        player.Rb.linearVelocity = DodgeRange;
    }

    public override void PhysicalUpdate()
    {
    }
    public override void OnAnimationFinished()
    {
        Debug.Log("Dodge animation finished");
        
        canChanged = true;
    }
}