using UnityEngine;

public class Attack : PlayerState
{

    private AnimatorStateInfo curAni;
    private int ComboIndex = 0;
    public Attack(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        canChanged = false;
    }
    public override void Enter()
    {
        //Idle Animation code
        int hash = Animator.StringToHash(
        player.currentWeapon.status.attackAnimations[ComboIndex]);
        canChanged = false;
        player.Rb.linearVelocity = Vector3.zero; // 추가

        player.status.UseStamina(player.currentWeapon.status.guardStamina);
    }

    public override void Exit()
    {
       
        if(player.bufferinput == StateType.Attack)
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
        // 애니메이션이 끝나기 70퍼 상태부터 상태전이 가능
        curAni = player.animator.GetCurrentAnimatorStateInfo(0);

        if(curAni.IsTag("Attack") && curAni.normalizedTime >= 0.75f)
        {

        }        


    }

    public override void PhysicalUpdate()
    {
        //이 상태에서 처리할만한 물리가 있나? 데미지는 트리거로 처리할꺼고
        
    }
    public override void OnAnimationFinished()
    {
        
        if (player.bufferinput == StateType.Attack)
        {
            canChanged = true;
        }
        curAni = player.animator.GetCurrentAnimatorStateInfo(0);
        if(curAni.normalizedTime >= 0.9f)
        {
            canChanged = true;
        }
    }
}
