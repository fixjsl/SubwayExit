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
        player.animator.CrossFade(player.attackHashes[ComboIndex],0.02f);
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
            if(player.bufferinput == StateType.Attack)
            {
                canChanged = true;
            }
        }        

        if(curAni.normalizedTime >=1.0f && !player.animator.IsInTransition(0))
        {
            canChanged = true;
        }
    }

    public override void PhysicalUpdate()
    {
        //이 상태에서 처리할만한 물리가 있나? 데미지는 트리거로 처리할꺼고
        
    }
}
