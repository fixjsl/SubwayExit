using Unity.VisualScripting;
using UnityEngine;

public class Dodge : PlayerState
{
    private float cooltime;
    private float lastTime;
    

    public Dodge(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        cooltime = player.status.DodgeCooldown;
        lastTime = -99f;
        isBlock = true;
    }

    public override bool CanEnter()
    {
        return Time.time >= lastTime + cooltime && player.status.Stamina >= player.status.DodgeCost; ;
    }
    public override void Enter()
    {

        lastTime = Time.time;
        canChanged = false;
        player.gameObject.layer = LayerMask.NameToLayer("Dodge");
        player.animator.CrossFade(player.dodge, 0.02f);

    }

    public override void Exit()
    {
        // 혹시 모를 경우 대비 Exit에서도 복구
        player.gameObject.layer = LayerMask.NameToLayer("Player");
    }


    public override void LogicUpdate()
    {
        // input Logic

    }

    public override void PhysicalUpdate()
    {
        float dir = Vector3.Dot(player.transform.forward, Vector3.right);
        player.Rb.linearVelocity = new Vector3(
            dir * player.status.dodgeSpeed,
            player.Rb.linearVelocity.y,
            0f);

    }
    public override void OnAnimationFinished()
    {
        canChanged = true; // 자동 탈출
    }
}