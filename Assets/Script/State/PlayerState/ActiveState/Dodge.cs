
using System.Security;
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
        player.status.UseStamina(player.status.DodgeCost);
        player.animator.applyRootMotion = false;
        player.Rb.isKinematic = false;
        player.animator.CrossFade(player.dodge, 0.15f);

    }

    public override void Exit()
    {
        // Ȥ�� �� ��� ��� Exit������ ����
        player.gameObject.layer = LayerMask.NameToLayer("Player");
        player.Rb.linearVelocity = new Vector3(0f, player.Rb.linearVelocity.y, 0f);
    }


    public override void LogicUpdate()
    {

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
        Debug.Log("Dodge animation finished");
        canChanged = true;
    }
}