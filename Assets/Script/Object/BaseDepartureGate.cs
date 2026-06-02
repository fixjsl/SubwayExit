using UnityEngine;

// 기지 출발 게이트 — 한번 나가면 비상구로만 귀환 가능
public class BaseDepartureGate : ItObjectBase
{
    [SerializeField] private Transform[] explorationStartPoints;
    [SerializeField] private float spawnZ = -3f;
    [SerializeField] private string warningMessage =
        "기지를 떠나면 비상구를 통해서만 돌아올 수 있습니다.\n출발하시겠습니까?";

    private bool departed = false;

    public override bool isStuck => false;
    public override string InteractMessage =>
        departed ? "" : $"출발 [{InputBindings.Interact}]";

    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        isInteracting = false;
        if (departed) return;
        ConfirmUI.Instance.Show(warningMessage, OnConfirmed);
    }

    private void OnConfirmed()
    {
        departed = true;
        RefreshPrompt();

        if (explorationStartPoints == null || explorationStartPoints.Length == 0) return;
        int idx = Random.Range(0, explorationStartPoints.Length);
        Vector3 raw = explorationStartPoints[idx].position;
        Vector3 startPos = new Vector3(raw.x, raw.y, spawnZ);

        var player = PlayerStateMachine.Instance;
        player.Rb.position = startPos;
        player.Rb.linearVelocity = Vector3.zero;

        // 시작지점에서 가장 먼 위치에 비상구 배치
        CycleManager.Instance.RelocateExitFarthestFrom(startPos);
    }

    // CycleManager가 기지 귀환 후 호출
    public void ResetForCycle()
    {
        departed = false;
        RefreshPrompt();
    }
}
