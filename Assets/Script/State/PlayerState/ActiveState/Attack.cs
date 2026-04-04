using UnityEngine;

public class Attack : PlayerState
{

    private AnimatorStateInfo curAni;
    private bool canCombo;
    private int ComboIndex = 0;
    public Attack(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        canChanged = false;
        canCombo = false;
    }
    public override void Enter()
    {
        //Idle Animation code
        
        canChanged = false;
        canCombo = false;
        player.Rb.linearVelocity = Vector3.zero; // �߰�
        player.animator.CrossFade(player.attackHashes[ComboIndex], 0.15f,1);
        player.status.UseStamina(player.currentWeapon.status.attackStamina);
    }

    public override void Exit()
    {
        if(player.bufferinput == StateType.Attack && ComboIndex <2)
        {
            ComboIndex++;
        }
        else
        {
            ComboIndex = 0;
        }
    }
 
    public override void LogicUpdate()
    {
        // �ִϸ��̼��� ������ 70�� ���º��� �������� ����
        if (canCombo && player.bufferinput == StateType.Attack)
        {
            canChanged = true;
        }       


    }

    public override void PhysicalUpdate()
    {
        //�� ���¿��� ó���Ҹ��� ������ �ֳ�? �������� Ʈ���ŷ� ó���Ҳ���
        
    }
    public override void OncanCombo()
    {
        canCombo = true;
    }
}
