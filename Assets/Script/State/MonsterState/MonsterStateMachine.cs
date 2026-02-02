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

    private State ActiveState;
    public State PassiveState { get; private set; }
    public Rigidbody Rb { get; private set; }
    public Animator animator { get; private set; }
    public MonsterStatus status;
    public readonly int idle = Animator.StringToHash("idle");
    public readonly int move = Animator.StringToHash("move");
    public readonly int sprint = Animator.StringToHash("chase");
    public readonly int hit = Animator.StringToHash("hit");
    public readonly int die = Animator.StringToHash("die");

    public readonly int[] attackHashes = {
    Animator.StringToHash("attack1"),
    Animator.StringToHash("attack2"),
    Animator.StringToHash("attack3")
    };

    public Dictionary<System.Type, State> Statecaches = new Dictionary<System.Type, State>();
    public void stateInit()
    {
        var StateT = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(MonsterState)) && !t.IsAbstract);
        Debug.Log($"발견된 상태 개수: {StateT.Count()}");
        foreach (var type in StateT)
        {
            try
            {
                var Sinstance = Activator.CreateInstance(type, new object[] { this }) as MonsterState;
                Statecaches.Add(type, Sinstance);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{type.Name} 클래스의 생성자가 잘못되었습니다! : {e.Message}");
            }
        }

        ActiveState = Statecaches[typeof(Idle)];
        ActiveState.Enter();
    }
    void Awake()
    {
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
    private void OnEnable()
    {
        DetectCorutine = this.StartCoroutine(Detect());
    }
    private void OnDisable()
    {
        if (DetectCorutine != null)
        {
            StopCoroutine(DetectCorutine);
            DetectCorutine = null;
        }
    }
    IEnumerator Detect()
    {
        WaitForSeconds wait = YeildCache.GetIntervals(0.5f);
        while (true)
        {
            Collider[] hitPlayers = Physics.OverlapSphere(transform.position, status.detect_range, playerLayer);

            if (hitPlayers.Length > 0)
            {
                // 플레이어를 찾음!
                var player = hitPlayers[0].GetComponent<PlayerStateMachine>();

                // 2. 여기서 이제 우리가 짰던 빛/소음 계산 함수를 돌립니다.
                float awareness = CalculateSoundAwareness(player);

                // 인지 게이지 상승 로직...



                // 2. 인지 게이지 상승
                status.detection_gauge += awareness * 0.1f; // 인터벌(0.1s) 보정

                // 3. 임계치 도달 시 상태 전환
                if (status.detection_gauge >= 100f)
                {
                    ChangeState<Chase>();
                    yield break;
                }

                // 4. 아무것도 감지 안 될 때 게이지 자연 감소
                if (awareness <= 0)
                {
                    status.detection_gauge -= status.recovery * 0.1f;
                }

            }
          
                yield return wait;
           }

        }
    float CalculateSoundAwareness(PlayerStateMachine player)
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        float noise = player.status.currentnoise;

        // 장애물이 있는지 체크 (Linecast)
        if (Physics.Linecast(transform.position, player.transform.position, obstacleMask))
        {
            noise *= 0.2f; // 벽이 있으면 소음 80% 감쇄
        }

        return (noise / Mathf.Max(dist * dist, 1f));
    }

}
