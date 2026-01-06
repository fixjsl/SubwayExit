using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
public enum StateType
{
    None, idle, Dodge, Attack, interect,Parry
}
public class PlayerStateMachine : MonoBehaviour, StateMachine
{
    //플레이어 키세팅
    private InputSystem_Actions action;
    //플레이어 객체 및 스탯
    public Rigidbody Rb { get; private set; }
    public Animator animator{  get; private set; }
    public PlayerStatus status = new PlayerStatus();
    // 현재 플레이어의 상태
    public float MoveInput;

    private State ActiveState;
    private List<State> PassiveStates = new List<State>();
    public bool isGuard { get; private set; }
    public bool isSprint {  get; private set; }
    public bool isCrunch { get; private set; }
    //플레이어 상태 캐싱
    public Dictionary<System.Type, State> Statecaches = new Dictionary<System.Type, State>();
    //애니메이션 상태 해싱
    public int idle { get; private set; } = Animator.StringToHash("idle");
    public int move { get; private set; } = Animator.StringToHash("move");
    public readonly int moveTurn = Animator.StringToHash("moveTurn");
    public int hit { get; private set; } = Animator.StringToHash("hit");
    public int die { get; private set; } = Animator.StringToHash("die");
    public int attack1 { get; private set; } = Animator.StringToHash("attack1");
    public int attack2 { get; private set; } = Animator.StringToHash("attack2");
    public int attack3 { get; private set; } = Animator.StringToHash("attack3");
    public int sprint { get; private set; } = Animator.StringToHash("sprint");
    public int sprintTrun { get; private set; } = Animator.StringToHash("sprintTurn");
    public int crunch { get; private set; } = Animator.StringToHash("crunch");
    public int parrying { get; private set; } = Animator.StringToHash("parrying");
    public int guard { get; private set; } = Animator.StringToHash("guard");
    public int dodge { get; private set; } = Animator.StringToHash("dodge");
    //플레이어 인풋버퍼
    public StateType bufferinput { get; private set; }
    public float buffertime { get; private set; } = 0.2f;
    public TimeManager bufferTimer = new TimeManager();
    //최초 상태 설정
    public void stateInit()
    {
        action = new InputSystem_Actions();
        action.PlayerAction.Attack.performed += _ => SetBuffer(StateType.Attack);
        action.PlayerAction.Dodge.performed += _ => SetBuffer(StateType.Dodge);
        action.PlayerAction.Interact.performed += _ => SetBuffer(StateType.interect);

        action.PlayerAction.Move.performed += ctx => MoveInput = ctx.ReadValue<float>();
        action.PlayerAction.Move.canceled += ctx => MoveInput = 0f;

        action.PlayerAction.Crouch.performed += _ => isCrunch = !isCrunch;
        action.PlayerAction.Sprint.performed += _ => isSprint = true;
        action.PlayerAction.Guard.performed += _ =>
        {
            isGuard = true;
            SetBuffer(StateType.Parry);
        };
        action.PlayerAction.Sprint.canceled += _ => isSprint = false;
        action.PlayerAction.Guard.canceled += _ => isGuard = false;
        var StateT = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(State)) && !t.IsAbstract);
        Debug.Log($"발견된 상태 개수: {StateT.Count()}");
        foreach (var type in StateT)
        {
            try
            {
                var Sinstance = Activator.CreateInstance(type, new object[] { this }) as State;
                Statecaches.Add(type, Sinstance);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{type.Name} 클래스의 생성자가 잘못되었습니다! : {e.Message}");
            }
        }

        status.OnDie += () => ChangeState<Die>();

        ActiveState = Statecaches[typeof(Idle)];
        ActiveState.Enter();
    }



    void Awake() {
        Rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stateInit();
    }
    void Update()
    {
        
        if (bufferinput != StateType.None)
        {
           if (bufferTimer.Timer(buffertime))
            {
            bufferinput = StateType.None;
            } 
        }

        CheckStateChange();



        ActiveState?.LogicUpdate();
        foreach (var passive in PassiveStates)
        {
            passive.LogicUpdate();
        }
    }

    void FixedUpdate()
    {
        ActiveState?.PhysicalUpdate();
        foreach (var passive in PassiveStates)
        {
            passive. PhysicalUpdate();
        }
    }
    //하위 상태들의 상태 변경 제공 함수
    public void ChangeState<T>() where T : State
    {
        System.Type type = typeof(T);

        if (!Statecaches.TryGetValue(type, out State nextState))
        {
            Debug.LogError($"{type.Name} 상태가 캐시에 존재하지 않습니다!");
            return;
        }

        ActiveState?.Exit();
        ActiveState = Statecaches[typeof(T)];
        Debug.Log($"change {ActiveState.ToString()} ");
        ActiveState?.Enter();
    }
    public bool CheckStateChange()
    {
        if (ActiveState.canChanged)
        {
            if((ActiveState is Guard || ActiveState is Parry) && !isGuard)
            {
                ChangeState<Idle>();
                return true ;
            }
            if(ActiveState is Parry && isGuard)
            {
                ChangeState<Guard>();
                return true ;
            }

            if (bufferinput != StateType.None)
            {
                Debug.Log($"current Buffer = {bufferinput}");
                BufferState();
                return true;
            }

            if (MoveInput != 0f)
            {
                if (isSprint)
                {
                    if (ActiveState is not Sprint) ChangeState<Sprint>();
                }
                else
                {
                    if (ActiveState is not Move) ChangeState<Move>();
                }
                return true;
            }
            if (ActiveState is not Idle)
            {
                ChangeState<Idle>();
                return true;
            }
        }
        return false;
    }
    public void BufferState()
    {
        switch (bufferinput)
        {
            case StateType.Attack: ChangeState<Attack>(); break;
            case StateType.Parry: ChangeState<Parry>(); break;
            case StateType.Dodge:
                {
                    if (Statecaches[typeof(Dodge)].CanEnter())
                    {
                        ChangeState<Dodge>();
                    }
                    ChangeState<Idle>();
                    break;
                }
            case StateType.interect: ChangeState<Interect>(); break;
            default: ChangeState<Idle>(); break;
        }
        ConsumeBuffer(bufferinput);
    }
    //선입력 함수
    public void SetBuffer(StateType buffertag)
    {
        bufferinput = buffertag;
        bufferTimer.Reset();
    }
    public bool ConsumeBuffer(StateType buffertag)
    {
        if(bufferinput == buffertag)
        {
            bufferinput = StateType.None;
            return true;
        }
        return false;
    }
    //PassiveList에 상태 넣거나 빼기
    public void AddpassiveStat<T>() where T : State
    {
        State passive = Statecaches[typeof(T)];

        if (PassiveStates.Contains(passive)) return;

        PassiveStates.Add(passive);
        passive.Enter();
    }
    public void RemovepassiveStat<T>() where T : State
    {
        State passive = Statecaches[typeof(T)];

        if (PassiveStates.Contains(passive)) {
            passive.Exit();
            PassiveStates.Remove(passive);
        }
    }
    //action활성화

    public void OnEnable()
    {
        action?.Enable();
    }
    public void OnDisable()
    {
        action?.Disable();
    }

    //데미지 이벤트 호출 함수
    public void OnHit(float damage)
    {
        if (ActiveState is Parry || ActiveState is Guard)
        {
            ActiveState.HandleDamage(damage);
            return;
        }
        ChangeState<hit>();
        ActiveState.HandleDamage(damage);

    }

}
