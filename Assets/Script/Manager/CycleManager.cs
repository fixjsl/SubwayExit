using UnityEngine;

public class CycleManager : MonoBehaviour
{
    public static CycleManager Instance { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics() => Instance = null;

    [SerializeField] private Transform playerBasePosition;
    [SerializeField] private BaseReturnExit activeExit;
    [SerializeField] private BaseDepartureGate departureGate;
    // 씬에 빈 오브젝트로 배치한 비상구 등장 가능 위치들
    [SerializeField] private Transform[] exitSpawnPoints;

    void Awake()
    {
        Instance = this;
    }

    public void ExecuteCycleReset()
    {
        TeleportPlayerToBase();
        RespawnMonsters();
        ResetContainers();
        if (departureGate != null) departureGate.ResetForCycle();
    }

    // BaseDepartureGate에서 시작지점 선택 후 호출 — 가장 먼 위치에 비상구 배치
    public void RelocateExitFarthestFrom(Vector3 origin)
    {
        if (activeExit == null || exitSpawnPoints == null || exitSpawnPoints.Length == 0) return;

        int bestIndex = 0;
        float bestDist = -1f;
        for (int i = 0; i < exitSpawnPoints.Length; i++)
        {
            float dist = Vector3.Distance(origin, exitSpawnPoints[i].position);
            if (dist > bestDist) { bestDist = dist; bestIndex = i; }
        }

        activeExit.transform.position = exitSpawnPoints[bestIndex].position;
        activeExit.ResetForCycle();
    }

    private void TeleportPlayerToBase()
    {
        if (playerBasePosition == null) return;
        var player = PlayerStateMachine.Instance;
        if (player == null) return;
        player.Rb.position = playerBasePosition.position;
        player.Rb.linearVelocity = Vector3.zero;
    }

    private void RespawnMonsters()
    {
        // 컴포넌트가 disabled된 몬스터도 포함해서 검색, 보스는 제외
        var monsters = FindObjectsByType<MonsterStateMachine>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var m in monsters)
        {
            if (m is BossStateMachine) continue;
            m.CycleRespawn();
        }
    }

    private void ResetContainers()
    {
        // SetActive(false)로 숨겨진 컨테이너도 포함
        var containers = FindObjectsByType<ContainerObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var c in containers)
            c.CycleReset();
    }

}
