using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MonsterStates;
using System.Collections;


public class MonsterStateMachine : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask playerLayer;
    public Coroutine DetectCorutine { get; private set; }
    public PlayerStateMachine Targetplayer {  get; private set; }
    public MonsterState ActiveState { get; private set; }
    public MonsterState PassiveState { get; private set; }
    public Rigidbody Rb { get; private set; }
    public Animator animator { get; private set; }
    public MonsterStatus status;
    public readonly int idle = Animator.StringToHash("idle");
    public readonly int move = Animator.StringToHash("move");
    public readonly int sprint = Animator.StringToHash("chase");
    public readonly int battle = Animator.StringToHash("battle");
    public readonly int hit = Animator.StringToHash("hit");
    public readonly int die = Animator.StringToHash("die");
    public readonly int moveTurn = Animator.StringToHash("moveTurn");
    public readonly int stun = Animator.StringToHash("stun");
    public readonly int[] attackHashes = {
    Animator.StringToHash("attack1"),
    Animator.StringToHash("attack2"),
    Animator.StringToHash("attack3")
    };
    [SerializeField] private LootTable lootTable;
    [SerializeField] private ItemPickup itemPickupPrefab;

    [Header("��������Ʈ �����ٶ�")]
    [SerializeField]
    public Transform spawnpoint;
    [SerializeField] private MonsterUI monsterUIPrefab;
    [SerializeField] private Transform hpUIPoint;
    [SerializeField] private Transform detectionUIPoint;
    public Dictionary<System.Type, MonsterState> Statecaches = new Dictionary<System.Type, MonsterState>();
    [SerializeField] private float hitAnimLength = 0.5f;
    public float HitAnimLength => hitAnimLength;
    [SerializeField] private Collider attackCollider; // �ν����� �Ҵ��

    public Collider AttackCollider => attackCollider; // �ܺο��� �б� ����
    public void stateInit()
    {
        var StateT = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(MonsterState)) && !t.IsAbstract);
        Debug.Log($"�߰ߵ� ���� ����: {StateT.Count()}");
        foreach (var type in StateT)
        {
            try
            {
                var Sinstance = Activator.CreateInstance(type, new object[] { this }) as MonsterState;
                Statecaches.Add(type, Sinstance);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{type.Name} Ŭ������ �����ڰ� �߸��Ǿ����ϴ�! : {e.Message}");
            }
        }

        ActiveState = Statecaches[typeof(MonsterStates.Idle)];
        ActiveState.Enter();
    }
    void Awake()
    {
        
        status = Instantiate(status);

        status.Hp = status.Maxhp;
        Rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        status.OnDie += () => ChangeState<MonsterStates.Die>();
        status.OnDie += SpawnLoot;
        stateInit();

        if (monsterUIPrefab != null)
        {
            var ui = Instantiate(monsterUIPrefab);
            ui.Init(this, hpUIPoint, detectionUIPoint);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public bool IsFrozen { get; private set; }

    public void Freeze()
    {
        IsFrozen = true;
        Rb.isKinematic = true;
    }

    public void Unfreeze()
    {
        IsFrozen = false;
        Rb.isKinematic = false;
    }

    void Update()
    {
        if (!IsFrozen) ActiveState?.LogicUpdate();
    }
    void FixedUpdate()
    {
        if (!IsFrozen) ActiveState?.PhysicalUpdate();
    }
    public void ChangeState<T>() where T : MonsterState
    {
        System.Type type = typeof(T);

        if (!Statecaches.TryGetValue(type, out MonsterState nextState))
        {
            Debug.LogError($"{type.Name} ���°� ĳ�ÿ� �������� �ʽ��ϴ�!");
            return;
        }

        ActiveState?.Exit();
        ActiveState = Statecaches[typeof(T)];
        ActiveState?.Enter();
    }
    public void OnHit(float Damage, float stunStrength)
    {
        if (ActiveState.isBlock)
        {
            ActiveState?.HandleDamage(Damage);
            return;
        }
        var hitState = Statecaches[typeof(MonsterStates.Hit)] as MonsterStates.Hit;
        hitState.SetHitduration(stunStrength);
        ChangeState<MonsterStates.Hit>();
        ActiveState?.HandleDamage(Damage);

    }
    public void OnExeHit(float Damage)
    {
        ActiveState?.HandleDamage(Damage);
        if(Targetplayer == null)
        {
            SetTarget(PlayerStateMachine.Instance);
            ChangeState<Battle>();
        }
    }
    private void OnEnable()
    {
        StartDetection();
    }
    private void OnDisable()
    {
        StopDetection();
    }
    public void StartDetection()
    {
        StopDetection(); // �ߺ� ���� ����
        DetectCorutine = StartCoroutine(Detect());
    }

    public void StopDetection()
    {
        if (DetectCorutine != null)
        {
            StopCoroutine(DetectCorutine);
            DetectCorutine = null;
        }
    }
    IEnumerator Detect()
    {
        WaitForSeconds wait = YeildCache.GetIntervals(0.1f);
        int lostFrames = 0;
        const int lostThreshold = 20; // 2초 유예
        while (true)
        {
            Collider[] hitPlayers = Physics.OverlapBox(transform.position, status.detectHalfExtents, Quaternion.identity, playerLayer);

            if (hitPlayers.Length > 0)
            {
                // �÷��̾ ã��!
                lostFrames = 0;
                Targetplayer = hitPlayers[0].GetComponent<PlayerStateMachine>();

                // 2. ���⼭ ���� �츮�� ®�� ��/���� ��� �Լ��� �����ϴ�.
                // 근접 + 빛 → 즉시 감지
                float dist = Vector3.Distance(transform.position, Targetplayer.transform.position);
                bool instantDetect = Targetplayer.status.currentbrighten > 0f
                    && dist <= status.instantDetectRange
                    && !Physics.Linecast(transform.position, Targetplayer.transform.position, obstacleMask);

                if (instantDetect)
                {
                    status.detection_gauge = 100f;
                }
                else
                {
                    float awareness = CalculateSoundAwareness(Targetplayer) + CalculateLightAwareness(Targetplayer);
                    status.detection_gauge += awareness * status.detectionSpeed * 0.1f;

                    if (awareness <= 0)
                        status.detection_gauge -= status.recovery * 0.1f;
                }

                // 감지 게이지 초과 시 Chase 전환
                if (status.detection_gauge >= 100f)
                {
                    status.detection_gauge = 0f;
                    if (ActiveState is MonsterStates.Idle || ActiveState is MonsterStates.Move || ActiveState is MonsterStates.Return)
                    {
                        ChangeState<Chase>();
                    }
                }

            }
            else
            {
                if (Targetplayer != null)
                {
                    // 순찰 중이면 멈추고 경계
                    if (lostFrames == 0 && ActiveState is MonsterStates.Move)
                        ChangeState<MonsterStates.Idle>();

                    lostFrames++;
                    if (lostFrames >= lostThreshold)
                    {
                        lostFrames = 0;
                        LoseTarget();
                    }
                }
            }

            yield return wait;
           }

        }
    float CalculateSoundAwareness(PlayerStateMachine player)
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        float noise = player.status.currentnoise;

        // ��ֹ��� �ִ��� üũ (Linecast)
        if (Physics.Linecast(transform.position, player.transform.position, obstacleMask))
        {
            noise *= 0.2f; // ���� ������ ���� 80% ����
        }

        return noise / Mathf.Max(dist, 1f);
    }
    float CalculateLightAwareness(PlayerStateMachine player)
    {
        if (Physics.Linecast(transform.position, player.transform.position, obstacleMask))
            return 0f;

        float dist = Vector3.Distance(transform.position, player.transform.position);
        return player.status.currentbrighten / Mathf.Max(dist, 1f);
    }
    public bool IsInBattleRange()
    {
        if (Targetplayer == null) return false;
        Vector3 delta = Targetplayer.transform.position - transform.position;
        return Mathf.Abs(delta.x) <= status.battleHalfExtents.x &&
               Mathf.Abs(delta.y) <= status.battleHalfExtents.y;
    }

    public bool CanBeExecuted() => status.detection_gauge <= 50f;

    public void SetTarget(PlayerStateMachine target)
    {
        Targetplayer = target;
    }

    public void LoseTarget()
    {
        Targetplayer = null;
        // � ���¿� �ֵ� Ÿ���� ������ ����
        ChangeState<Return>();
    }

    public void ChangeStun()
    {
        ChangeState<Stun>();
    }

    private void SpawnLoot()
    {
        if (lootTable == null || itemPickupPrefab == null) return;
        var drops = lootTable.Roll();
        for (int i = 0; i < drops.Count; i++)
        {
            var pos = transform.position + Vector3.up * 0.5f + Vector3.right * (i * 0.5f);
            var pickup = Instantiate(itemPickupPrefab, pos, Quaternion.identity);
            pickup.Setup(drops[i].item, drops[i].count);
        }
    }

    void OnAnimatorMove()
    {
        Rb.MoveRotation(Rb.rotation * animator.deltaRotation);
    }

#if UNITY_EDITOR
    [SerializeField] private bool debugMode = false;

    void Start()
    {
        if (debugMode) StartCoroutine(DebugStatusCoroutine());
    }

    private IEnumerator DebugStatusCoroutine()
    {
        var wait = new WaitForSeconds(0.5f);
        while (true)
        {
            yield return wait;

            string targetName = Targetplayer != null ? Targetplayer.name : "없음";
            float dist = Targetplayer != null
                ? Vector3.Distance(transform.position, Targetplayer.transform.position)
                : -1f;

            Debug.Log(
                $"[MONSTER DEBUG] {gameObject.name}\n" +
                $"  ActiveState     : {ActiveState?.GetType().Name ?? "null"}\n" +
                $"  HP              : {status.Hp:F0} / {status.Maxhp}\n" +
                $"  DetectionGauge  : {status.detection_gauge:F1} / 100\n" +
                $"  Target          : {targetName}\n" +
                $"  TargetDist      : {(dist >= 0 ? dist.ToString("F1") : "N/A")}\n" +
                $"  DetectRange     : {status.detectHalfExtents}\n" +
                $"  BattleRange     : {status.battleHalfExtents}\n" +
                $"  --- 물리 ---\n" +
                $"  Velocity        : {Rb.linearVelocity}\n" +
                $"  Position        : {Rb.position}\n" +
                $"  IsKinematic     : {Rb.isKinematic}\n" +
                $"  Constraints     : {Rb.constraints}\n" +
                $"  Rotation(Y)     : {Rb.rotation.eulerAngles.y:F1}"
            );
        }
    }
    void OnDrawGizmos()
    {
        // 감지 범위 (노랑)
        Gizmos.color = Color.yellow;
        if (status != null) Gizmos.DrawWireCube(transform.position, status.detectHalfExtents * 2f);

        // 전투 범위 (빨강)
        Gizmos.color = Color.red;
        if (status != null) Gizmos.DrawWireCube(transform.position, status.battleHalfExtents * 2f);

        // 공격 범위 (마젠타)
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, status != null ? status.atk_range : 0f);
    }
#endif

}
