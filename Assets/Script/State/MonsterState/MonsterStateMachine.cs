using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using MonsterStates;


public class MonsterStateMachine : MonoBehaviour
{
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
}
