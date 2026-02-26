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
        canChanged = false;
        isBlock = true;
    }

    public override bool CanEnter()
    {
        return Time.time >= lastTime + cooltime;
    }
    public override void Enter()
    {

        lastTime = Time.time;
        //Idle Animation code
        player.animator.CrossFade(player.dodge, 0.02f);
        
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