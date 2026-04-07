using System.Security;
using UnityEngine;

public class Idle : PlayerState
{



    public Idle(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
         //speed init
        player.Rb.linearVelocity = Vector3.zero;
        //Idle Animation code
        player.animator.CrossFade(player.idle, 0.2f); // 0번 레이어
        player.animator.CrossFade(player.idle, 0.2f, 1); // 1번 레이어, idle이 있다면

    }

    public override void Exit()
    {
        //Stop Idle Animation code
        
    }



    public override void LogicUpdate()
    {
        //���⵵ �� ���ڳ� ���߿� �����Ǹ� �ִϸ��̼� �߰��� �� ����
        if(player.status.Stamina != player.status.MaxStamina)
        {
            player.status.Stamina += player.status.staminaRecoverey * Time.deltaTime;
        }
    }

    public override void PhysicalUpdate()
    {


    }
}
