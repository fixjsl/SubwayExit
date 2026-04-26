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
    public Vector3 spawnpoint;
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
    void Update()
    {
        ActiveState?.LogicUpdate();
    }
    void FixedUpdate()
    {
        ActiveState?.PhysicalUpdate();
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
        const int lostThreshold = 5; // 0.5초 유예
        while (true)
        {
            Collider[] hitPlayers = Physics.OverlapSphere(transform.position, status.detect_range, playerLayer);

            if (hitPlayers.Length > 0)
            {
                // �÷��̾ ã��!
                lostFrames = 0;
                Targetplayer = hitPlayers[0].GetComponent<PlayerStateMachine>();

                // 2. ���⼭ ���� �츮�� ®�� ��/���� ��� �Լ��� �����ϴ�.
                float awareness = CalculateSoundAwareness(Targetplayer) + CalculateLightAwareness(Targetplayer);

                // ���� ������ ��� ����...



                // 2. ���� ������ ���
                status.detection_gauge += awareness * 0.1f; // ���͹�(0.1s) ����

                // 3. 감지 게이지 초과 시 Chase 전환
                if (status.detection_gauge >= 100f)
                {
                    status.detection_gauge = 0f;
                    if (ActiveState is MonsterStates.Idle || ActiveState is MonsterStates.Move || ActiveState is MonsterStates.Return)
                    {
                        ChangeState<Chase>();
                    }
                }

                // 4. �ƹ��͵� ���� �� �� �� ������ �ڿ� ����
                if (awareness <= 0)
                {
                    status.detection_gauge -= status.recovery * 0.1f;
                }

            }
            else
            {
                // ���� ������ ������ Ÿ�� �ҽ�
                if (Targetplayer != null)
                {
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

        return (noise / Mathf.Max(dist * dist, 1f));
    }
    float CalculateLightAwareness(PlayerStateMachine player)
    {
        if (Physics.Linecast(transform.position, player.transform.position, obstacleMask))
            return 0f;

        float distSq = (player.transform.position - transform.position).sqrMagnitude;
        return player.status.currentbrighten / Mathf.Max(distSq, 1f);
    }
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
                $"  DetectRange     : {status.detect_range}\n" +
                $"  BattleRange     : {status.battle_range}\n" +
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
        Gizmos.DrawWireSphere(transform.position, status != null ? status.detect_range : 0f);

        // 전투 범위 (빨강)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, status != null ? status.battle_range : 0f);

        // 공격 범위 (마젠타)
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, status != null ? status.atk_range : 0f);
    }
#endif

}
