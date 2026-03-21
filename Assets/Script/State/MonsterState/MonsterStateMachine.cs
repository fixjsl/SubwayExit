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
    public readonly int Stun = Animator.StringToHash("Stun");
    public readonly int[] attackHashes = {
    Animator.StringToHash("attack1"),
    Animator.StringToHash("attack2"),
    Animator.StringToHash("attack3")
    };
    [Header("��������Ʈ �����ٶ�")]
    [SerializeField]
    public Vector3 spawnpoint;

    public Dictionary<System.Type, MonsterState> Statecaches = new Dictionary<System.Type, MonsterState>();
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
        stateInit();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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
        Debug.Log($"change {ActiveState.ToString()} ");
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
        while (true)
        {
            Collider[] hitPlayers = Physics.OverlapSphere(transform.position, status.detect_range, playerLayer);

            if (hitPlayers.Length > 0)
            {
                // �÷��̾ ã��!
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
                if (Targetplayer != null) LoseTarget();
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
        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        if (Vector3.Dot(transform.forward, dirToPlayer) <= 0f) return 0f;

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


}
