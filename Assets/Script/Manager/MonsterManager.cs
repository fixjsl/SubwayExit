using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum MonsterType {}
public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }
    // 활성화된 몬스터 리스트
    private List<MonsterStateMachine> activeMonsters = new List<MonsterStateMachine>();

    // 비활성화된 몬스터 풀 (프리팹 타입별)
    private Dictionary<MonsterType, Queue<MonsterStateMachine>> pool = new Dictionary<MonsterType, Queue<MonsterStateMachine>>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void Preload(MonsterType type, GameObject prefab, int count, Transform parent = null)
    {
        if (!pool.ContainsKey(type))
            pool[type] = new Queue<MonsterStateMachine>();

        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab, parent);
            var monster = obj.GetComponent<MonsterStateMachine>();
            obj.SetActive(false);
            pool[type].Enqueue(monster);
        }
    }
    public void Register(MonsterStateMachine monster)
    {
        if (!activeMonsters.Contains(monster))
            activeMonsters.Add(monster);
    }

    // 몬스터 사망 시 풀로 반환
    public void ReturnToPool(MonsterStateMachine monster, MonsterType key)
    {
        activeMonsters.Remove(monster);
        monster.gameObject.SetActive(false);

        if (!pool.ContainsKey(key))
            pool[key] = new Queue<MonsterStateMachine>();

        pool[key].Enqueue(monster);
    }
    public MonsterStateMachine Spawn(MonsterType type, Vector3 position)
    {
        var monster = pool[type].Dequeue();
        monster.transform.position = position;
        monster.spawnpoint = position; // 스폰포인트도 같이 지정
        monster.gameObject.SetActive(true);
        Register(monster);
        return monster;
    }
    // 활성 몬스터 중 범위 내 스턴 상태 탐색
    public MonsterStateMachine GetStunnedInRange(Vector3 origin, float range)
    {
        float rangeSq = range * range;
        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;
            if ((monster.transform.position - origin).sqrMagnitude > rangeSq) continue;
            if (monster.ActiveState is MonsterStates.Stun)
                return monster;
        }
        return null;
    }

    // 활성 몬스터 중 범위 내 기습 가능 상태 탐색
    public MonsterStateMachine GetAmbushTargetInRange(Vector3 origin, float range, float gaugeLimit)
    {
        float rangeSq = range * range;
        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;
            if ((monster.transform.position - origin).sqrMagnitude > rangeSq) continue;
            if (monster.status.detection_gauge > gaugeLimit) continue;
            if (monster.ActiveState is MonsterStates.Idle || monster.ActiveState is MonsterStates.move)
                return monster;
        }
        return null;
    }
}
