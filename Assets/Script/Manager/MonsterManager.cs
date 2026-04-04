using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum MonsterType {}
public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }
    // Ȱ��ȭ�� ���� ����Ʈ
    private List<MonsterStateMachine> activeMonsters = new List<MonsterStateMachine>();

    // ��Ȱ��ȭ�� ���� Ǯ (������ Ÿ�Ժ�)
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

    // ���� ��� �� Ǯ�� ��ȯ
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
        if(!pool.ContainsKey(type) || pool[type].Count == 0) return null;
        var monster = pool[type].Dequeue();
        monster.transform.position = position;
        monster.spawnpoint = position; 
        monster.gameObject.SetActive(true);
        Register(monster);
        return monster;
    }
    // Ȱ     Ž
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

    // Ȱ�� ���� �� ���� �� ��� ���� ���� Ž��
    public MonsterStateMachine GetAmbushTargetInRange(Vector3 origin, float range, float gaugeLimit)
    {
        float rangeSq = range * range;
        foreach (var monster in activeMonsters)
        {
            if (monster == null) continue;
            if ((monster.transform.position - origin).sqrMagnitude > rangeSq) continue;
            if (monster.status.detection_gauge > gaugeLimit) continue;
            if (monster.ActiveState is MonsterStates.Idle || monster.ActiveState is MonsterStates.Move)
                return monster;
        }
        return null;
    }
}
