using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
    public Inventory inventory;
    public Weapon currentWeapon;
    [SerializeField] public Transform weaponSlot;
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private TwoBoneIKConstraint leftHandIK;
    [SerializeField] private TwoBoneIKConstraint rightHandIK;
    
    //÷
    public float MoveInput;
    float lastMoveInput;
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
    public readonly int excution = Animator.StringToHash("execution");
    public readonly int sprint = Animator.StringToHash("sprint");
    public readonly int sprintTurn = Animator.StringToHash("sprintTurn");
    public readonly int incrunch = Animator.StringToHash("incrunch");
    public readonly int outcrunch = Animator.StringToHash("outcrunch");
    public readonly int crunchTurn = Animator.StringToHash("crunchTurn");
    public readonly int crunch = Animator.StringToHash("crunch");
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

    private List<Iinterectable> nearbyInteractables = new List<Iinterectable>();
    public Iinterectable nearbyInteractable => GetClosest();
    public void SetInteractable(Iinterectable interactable)
    {
    if (!nearbyInteractables.Contains(interactable))
        nearbyInteractables.Add(interactable);
    }
    public void ClearInteractable(Iinterectable interactable)
    {
        nearbyInteractables.Remove(interactable);
    }
    private Iinterectable GetClosest()
{
    if (nearbyInteractables.Count == 0) return null;
    return nearbyInteractables
        .OrderBy(i => (((MonoBehaviour)i).transform.position - transform.position).sqrMagnitude)
        .First();
}
    //
    public void stateInit()
    {
        action = new InputSystem_Actions();
        action.PlayerAction.Attack.performed += _ => { if (currentWeapon != null) SetBuffer(StateType.Attack); };
        action.PlayerAction.Dodge.performed += _ => SetBuffer(StateType.Dodge);
        action.PlayerAction.Interact.performed += _ => { Debug.Log("Interact input received"); SetBuffer(StateType.interect); };
        action.PlayerAction.LightTogle.performed += _ => {  
           if(currentLight != null)
            {
                currentLight.enabled = !currentLight.enabled;
            }
        };
        action.PlayerAction.Move.performed += ctx => {
            float value = ctx.ReadValue<float>();
    // 반대 방향일 때만 업데이트
            if (lastMoveInput == 0f)
            {
                MoveInput = value;
                lastMoveInput = value;
            }
        };
        action.PlayerAction.Move.canceled += ctx =>
        {
            MoveInput = 0f;
            lastMoveInput = 0f;
        };

        action.PlayerAction.Crouch.performed += _ => isCrunch = !isCrunch;
        action.PlayerAction.Sprint.performed += _ => isSprint = true;
        action.PlayerAction.Guard.performed += _ =>
        {
            isGuard = true;
            if (currentWeapon != null)
            {
                SetBuffer(StateType.Parry);
            }
        };
        action.PlayerAction.Sprint.canceled += _ => isSprint = false;
        action.PlayerAction.Guard.canceled += _ => isGuard = false;
        action.Enable();
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
        status.StaminaEmpty += () => { if (ActiveState is not Move && ActiveState is not Idle && ActiveState is not Dodge) ChangeState<Move>(); };
        ActiveState = Statecaches[typeof(Idle)];
        ActiveState.Enter();
    }



    void Awake() {
        if(Instance == null) Instance = this;
        inventory = new Inventory(status);
        currentLight = GetComponentInChildren<Light>();
        Rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.SetLayerWeight(1, 0f);
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
        if(typeof(T) != ActiveState.GetType()  )
        {
            Debug.Log($"change {ActiveState.ToString()} -> {nextState.ToString()} ");
        }
        ActiveState?.Exit();
        bufferinput = StateType.None;
        ActiveState = Statecaches[typeof(T)];
        
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
        Debug.Log($"Buffer set: {buffertag}");
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

#if UNITY_EDITOR
    [SerializeField] private bool debugMode = false;

    void Start()
    {
        if (debugMode) StartCoroutine(DebugStatusCoroutine());
    }

    private IEnumerator DebugStatusCoroutine()
    {
        var wait = new WaitForSeconds(1f);
        while (true)
        {
            yield return wait;

            var info0 = animator.GetCurrentAnimatorStateInfo(0);
            bool inTransition0 = animator.IsInTransition(0);
            string animName0 = inTransition0
                ? $"{GetDebugAnimName(info0)} → {GetDebugAnimName(animator.GetNextAnimatorStateInfo(0))} (전환중)"
                : GetDebugAnimName(info0);

            var info1 = animator.GetCurrentAnimatorStateInfo(1);
            bool inTransition1 = animator.IsInTransition(1);
            float layer1Weight = animator.GetLayerWeight(1);
            string animName1 = layer1Weight == 0f ? "꺼짐" : inTransition1
                ? $"{GetDebugAnimName(info1)} → {GetDebugAnimName(animator.GetNextAnimatorStateInfo(1))} (전환중)"
                : GetDebugAnimName(info1);

            string passiveList = PassiveStates.Count > 0
                ? string.Join(", ", PassiveStates.ConvertAll(s => s.GetType().Name))
                : "없음";

            string interactList = nearbyInteractables.Count > 0
                ? string.Join(", ", nearbyInteractables.ConvertAll(i => ((MonoBehaviour)i).name))
                : "없음";

            Debug.Log(
                $"[DEBUG STATUS]\n" +
                $"  ActiveState  : {ActiveState?.GetType().Name ?? "null"}\n" +
                $"  canChanged   : {ActiveState?.canChanged}\n" +
                $"  Layer0 Anim  : {animName0}  (normalized: {info0.normalizedTime:F2})\n" +
                $"  Layer1 Anim  : {animName1}  (weight: {layer1Weight:F2})\n" +
                $"  PassiveStates: {passiveList}\n" +
                $"  BufferInput  : {bufferinput}\n" +
                $"  MoveInput    : {MoveInput}\n" +
                $"  isGuard / isSprint / isCrunch : {isGuard} / {isSprint} / {isCrunch}\n" +
                $"  NearbyInteractables ({nearbyInteractables.Count}): {interactList}"
            );
        }
    }

    private string GetDebugAnimName(AnimatorStateInfo info)
    {
        var map = new Dictionary<int, string>
        {
            { idle,       "idle"       }, { move,       "move"       },
            { moveTurn,   "moveTurn"   }, { hit,        "hit"        },
            { die,        "die"        }, { sprint,     "sprint"     },
            { sprintTurn, "sprintTurn" }, { incrunch,   "incrunch"   },
            { outcrunch,  "outcrunch"  }, { crunchTurn, "crunchTurn" },
            { crunch, "crunch" }, { crunchMove, "crunchMove" },
            { parrying,   "parrying"   }, { guard,      "guard"      },
            { dodge,      "dodge"      }
        };
        foreach (var kv in map)
            if (info.shortNameHash == kv.Key) return kv.Value;
        for (int i = 0; i < attackHashes.Length; i++)
            if (info.shortNameHash == attackHashes[i]) return $"attack{i + 1}";
        return $"unknown(hash:{info.shortNameHash})";
    }
#endif

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
        if (currentWeapon != null)
            Destroy(currentWeapon.gameObject);
        
        animator.SetLayerWeight(1, 1f);

        currentWeapon = weapon;
        weapon.SetPlayer(this);
        weapon.transform.SetParent(weaponSlot, true);
        if (weapon.primaryGrip != null)
        {
            // primaryGrip이 weaponSlot 위치/방향에 오도록 월드 공간에서 계산
            weapon.transform.rotation = weaponSlot.rotation * Quaternion.Inverse(weapon.primaryGrip.localRotation);
            weapon.transform.position += weaponSlot.position - weapon.transform.TransformPoint(weapon.primaryGrip.localPosition);
            if (rightHandIK != null) {
                rightHandIK.weight = 1f;
                Debug.Log(rightHandIK.weight);
            }
        }
        else
        {
            if (rightHandIK != null) rightHandIK.weight = 0f;
        }
        if (leftHandTarget != null && weapon.secondaryGrip != null)
        {
            leftHandTarget.SetParent(weapon.secondaryGrip);
            leftHandTarget.localPosition = Vector3.zero;
            leftHandTarget.localRotation = Quaternion.identity;
            if (leftHandIK != null) leftHandIK.weight = 1f;
        }
        else
        {
            if (leftHandIK != null) leftHandIK.weight = 0f;
        }
        
        // 오버라이드 애니메이터 적용
        if (weapon.status.WeaponAnimations != null)
        {
            animator.runtimeAnimatorController = weapon.status.WeaponAnimations;
        }
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
