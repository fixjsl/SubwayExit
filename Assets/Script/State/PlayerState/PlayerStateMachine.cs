using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

public enum StateType
{
    None, idle, Dodge, Attack, interect,Parry, crunch
}
public class PlayerStateMachine : MonoBehaviour
{

    public static PlayerStateMachine Instance { get; private set; }
    //�÷��̾� Ű����
    private InputSystem_Actions action;
    //�÷��̾� ��ü �� ����
    public Rigidbody Rb { get; private set; }
    public Animator animator{  get; private set; }
    public PlayerStatus status = new PlayerStatus();
    public Weapon currentWeapon;
    // ���� �÷��̾��� ����
    public float MoveInput;
    public Light currentLight {get; private set; }
    public PlayerState ActiveState { get; private set; }
    public List<PlayerState> PassiveStates { get; private set; } = new List<PlayerState>();
    public bool isGuard { get; private set; }
    public bool isSprint {  get; private set; }
    public bool isCrunch { get; private set; }
    //�÷��̾� ���� ĳ��
    public Dictionary<System.Type, PlayerState> Statecaches = new Dictionary<System.Type, PlayerState>();
    //�ִϸ��̼� ���� �ؽ�
    public readonly int idle  = Animator.StringToHash("idle");
    public readonly int move = Animator.StringToHash("move");
    public readonly int moveTurn = Animator.StringToHash("moveTurn");
    public readonly int hit  = Animator.StringToHash("hit");
    public readonly int die = Animator.StringToHash("die");
    public readonly int[] attackHashes = {
    Animator.StringToHash("attack1"),
    Animator.StringToHash("attack2"),
    Animator.StringToHash("attack3")
    };
    public readonly int sprint = Animator.StringToHash("sprint");
    public readonly int sprintTurn = Animator.StringToHash("sprintTurn");
    public readonly int incrunch = Animator.StringToHash("incrunch");
    public readonly int outcrunch = Animator.StringToHash("outcrunch");
    public readonly int crunchTurn = Animator.StringToHash("crunchTurn");
    public readonly int crunchIdle = Animator.StringToHash("crunchIdle");
    public readonly int crunchMove = Animator.StringToHash("crunchMove");
    public readonly int parrying = Animator.StringToHash("parrying");
    public readonly int guard = Animator.StringToHash("guard");
    public readonly int dodge = Animator.StringToHash("dodge");
    //�÷��̾� ��ǲ����
    public StateType bufferinput { get; private set; }
    public float buffertime { get; private set; } = 0.2f;
    public TimeManager bufferTimer = new TimeManager();
    //�и� ���� ����
    public bool isParrying => ActiveState is Parry parry && parry.IsInParryWindow;

    public Iinterectable nearbyInteractable { get; private set; }
    public void SetInteractable(Iinterectable interactable)
    {
        if (nearbyInteractable == null)
        {
            nearbyInteractable = interactable;
            return;
        }
        // �̹� ������ �� ����� �ɷ� ��ü
        float currentDist = (((MonoBehaviour)nearbyInteractable).transform.position - transform.position).sqrMagnitude;
        float newDist = (((MonoBehaviour)interactable).transform.position - transform.position).sqrMagnitude;
        if (newDist < currentDist) nearbyInteractable = interactable;
    }
    public void ClearInteractable() => nearbyInteractable = null;
    //���� ���� ����
    public void stateInit()
    {
        action = new InputSystem_Actions();
        action.PlayerAction.Attack.performed += _ => { if (currentWeapon != null) SetBuffer(StateType.Attack); };
        action.PlayerAction.Dodge.performed += _ => SetBuffer(StateType.Dodge);
        action.PlayerAction.Interact.performed += _ => SetBuffer(StateType.interect);
        action.PlayerAction.LightTogle.performed += _ => {  
           if(currentLight != null)
            {
                currentLight.enabled = !currentLight.enabled;
            }
        };
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
        var StateT = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(PlayerState)) && !t.IsAbstract);
        Debug.Log($"�߰ߵ� ���� ����: {StateT.Count()}");
        foreach (var type in StateT)
        {
            try
            {
                var Sinstance = Activator.CreateInstance(type, new object[] { this }) as PlayerState;
                Statecaches.Add(type, Sinstance);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{type.Name} Ŭ������ �����ڰ� �߸��Ǿ����ϴ�! : {e.Message}");
            }
        }
        AddpassiveStat<NoiseABright>();
        status.OnDie += () => ChangeState<Die>();
        status.StaminaEmpty += () => ChangeState<Move>();
        ActiveState = Statecaches[typeof(Idle)];
        ActiveState.Enter();
    }



    void Awake() {
        if(Instance == null) Instance = this;
        currentLight = GetComponentInChildren<Light>();
        Rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        stateInit();
    }
    void Update()
    {
        CheckStateChange();

        if (bufferinput != StateType.None)
        {
            if (bufferTimer.Timer(buffertime))
            {
                bufferinput = StateType.None;
            }
        }    
        ActiveState?.LogicUpdate();

    }

    void FixedUpdate()
    {
        ActiveState?.PhysicalUpdate();

    }
    //���� ���µ��� ���� ���� ���� �Լ�
    public void ChangeState<T>() where T : PlayerState
    {
        System.Type type = typeof(T);

        if (!Statecaches.TryGetValue(type, out PlayerState nextState))
        {
            Debug.LogError($"{type.Name} ���°� ĳ�ÿ� �������� �ʽ��ϴ�!");
            return;
        }
        
        ActiveState?.Exit();
        bufferinput = StateType.None;
        ActiveState = Statecaches[typeof(T)];
        Debug.Log($"change {ActiveState.ToString()} ");
        ActiveState?.Enter();
    }
    public bool CheckStateChange()
    {
        if (ActiveState.canChanged)
        {

            if (isCrunch && ActiveState is not Crunch)
            {
                ChangeState<Crunch>();
                return true;
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
            if((ActiveState is Guard || ActiveState is Parry) && !isGuard)
            {
                ChangeState<Idle>();
                return true ;
            }
            if (ActiveState is Crunch) return false;
            if (MoveInput != 0f)
            {
                if (ActiveState is not Move) ChangeState<Move>();
                
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
            case StateType.Attack:
                {
                    Collider[] hits = Physics.OverlapSphere(transform.position,
                         currentWeapon.status.attackRange, 1 << Layercache.Stun);
                    if (hits.Length > 0)
                    {
                        var execution = Statecaches[typeof(Execution)] as Execution;
                        execution.setTarget(hits[0].GetComponentInParent<MonsterStateMachine>());
                        ChangeState<Execution>();
                    }
                    else ChangeState<Attack>(); 
                    break;
                }
            case StateType.Parry: ChangeState<Parry>(); break;
            case StateType.Dodge:
                {
                    if (Statecaches[typeof(Dodge)].CanEnter())
                    {
                        ChangeState<Dodge>();
                    }
                    else ChangeState<Idle>();
                    break;
                }
            case StateType.interect: ChangeState<Interect>(); break;
            default: ChangeState<Idle>(); break;
        }
        ConsumeBuffer(bufferinput);
    }
    //���Է� �Լ�
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
    //PassiveList�� ���� �ְų� ����
    public void AddpassiveStat<T>() where T : PlayerState
    {
        PlayerState passive = Statecaches[typeof(T)];

        if (PassiveStates.Contains(passive)) return;

        PassiveStates.Add(passive);
        passive.Enter();
    }
    public void RemovepassiveStat<T>() where T : PlayerState
    {
        PlayerState passive = Statecaches[typeof(T)];

        if (PassiveStates.Contains(passive)) {
            passive.Exit();
            PassiveStates.Remove(passive);
        }
    }
    public void ClearpassiveStat()
    {
        foreach(var state in PassiveStates)
        {
            state.Exit();
        }
        PassiveStates.Clear();
    }
    //actionȰ��ȭ

    public void OnEnable()
    {
        action?.Enable();
    }
    public void OnDisable()
    {
        action?.Disable();
    }

    //������ �̺�Ʈ ȣ�� �Լ�
    public void OnHit(float Damage)
    {
        if (ActiveState.isBlock)
        {
            ActiveState?.HandleDamage(Damage);
            return;
        }
        ChangeState<Hit>();
        ActiveState?.HandleDamage(Damage);

    }
    public void EquipWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
        // ���� ���� �� �ʿ��� �߰� ������ ���⿡ �ۼ��� �� �ֽ��ϴ�.
    }
    public void EquipLight(Light newLight)
    {
        currentLight = newLight;
        (Statecaches[typeof(NoiseABright)] as NoiseABright).light = newLight;
    }
    //�ִϸ��̼� ȸ��
    void OnAnimatorMove()
    {
        Rb.MoveRotation(Rb.rotation * animator.deltaRotation);
    }
    // ========== �ִϸ��̼� �̺�Ʈ ==========

}
