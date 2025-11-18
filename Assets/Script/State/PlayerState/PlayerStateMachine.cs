using UnityEngine;

public class PlayerStateMachine : MonoBehaviour, StateMachine
{


    private PlayerAction actionF;

    private State ActiveState;
    private State PassiveState;

    //최초 상태 설정
    public void stateInit()
    {
        ActiveState = new Idle(this);
        PassiveState = new PassiveStateList(this);
        ActiveState.Enter();
        PassiveState.Enter();
    }

    private void comptInit()
    {
        actionF = GetComponent<PlayerAction>();
    }


    void Awake() {
        stateInit();
        comptInit();
    }
    void Update()
    {
        ActiveState?.HandleUpdate();
        PassiveState?.HandleUpdate();

        ActiveState?.LogicUpdate();
        PassiveState?.LogicUpdate();
    }

    void FixedUpdate()
    {
        ActiveState?.PsycialUpdate();
        PassiveState?.PsycialUpdate();
    }
    //하위 상태들의 상태 변경 제공 함수
    public void ChangeState(State state)
    {
        ActiveState?.Exit();
        ActiveState = state;
        ActiveState?.Enter();
    }


}
