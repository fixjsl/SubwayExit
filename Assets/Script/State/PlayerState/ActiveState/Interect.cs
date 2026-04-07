using UnityEngine;

public class Interect : PlayerState
{

    private Object interectObject;
    public Interect(PlayerStateMachine stateMachine) : base(stateMachine) {
    }
    public override void Enter()
    {
        if (player.nearbyInteractable == null)
        {
            Debug.LogWarning("Interact: 상호작용 대상이 없습니다.");
            canChanged = true;
            return;
        }
        player.nearbyInteractable.Oninterect(player.transform.position);
    }

    public override void Exit()
    {
        canChanged = true;
    }

}
