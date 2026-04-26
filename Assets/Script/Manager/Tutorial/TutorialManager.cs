using System.Collections;
using System.Net.NetworkInformation;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    private TutorialTriggerType? pendingTrigger = null;
    private MonsterStateMachine tutorialWolf;

    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private GameObject moveKeyUI;
    [SerializeField] private GameObject sprintKeyUI;
    [SerializeField] private GameObject interactKeyUI;
    [SerializeField] private GameObject attackKeyUI;

    [SerializeField] private Doorcs weaponRoomDoor;
    [SerializeField] private Transform playerStopPoint;
    [SerializeField] private GameObject weaponCardContainer;
    [SerializeField] private GameObject wolfPrefab;
    [SerializeField] private Transform wolfSpawnPoint;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private Transform monsterSpawnPoint;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(TutorialSequence());
    }

    public void OnTrigger(TutorialTriggerType type)
    {
        pendingTrigger = type;
    }

    // 특정 트리거가 들어올 때까지 대기
    IEnumerator WaitForTrigger(TutorialTriggerType type)
    {
        while (pendingTrigger != type)
            yield return new WaitForSecondsRealtime(0.05f);
        pendingTrigger = null;
    }

    // 특정 입력이 들어올 때까지 대기
    IEnumerator WaitForInput(string actionName)
    {
        var action = PlayerStateMachine.Instance.GetComponent<PlayerInput>()
            .actions.FindAction(actionName);
        bool pressed = false;
        action.performed += _ => pressed = true;
        while (!pressed)
            yield return null;
        action.performed -= _ => pressed = true;
    }

    // ===================== 메인 시퀀스 =====================

    IEnumerator TutorialSequence()
    {
        GameManager.Instance.TutorialStart();

        // 1단계: 이동키 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.ShowMoveKeyNdialogue);
        if (moveKeyUI != null) moveKeyUI.SetActive(true);

        // 2단계: 늑대 등장
        yield return WaitForTrigger(TutorialTriggerType.WolfFearNdialogue);
        if (moveKeyUI != null) moveKeyUI.SetActive(false);
        if (wolfPrefab != null && wolfSpawnPoint != null)
        {
            var wolfObj = Instantiate(wolfPrefab, wolfSpawnPoint.position, wolfPrefab.transform.rotation);
            if (wolfObj.TryGetComponent<MonsterStateMachine>(out var wolf))
            {
                tutorialWolf = wolf;
                wolf.SetTarget(PlayerStateMachine.Instance);
                wolf.ChangeState<MonsterStates.Chase>();
            }
        }

        // 3단계: 달리기 키 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.ShowSprintKey);
        if (sprintKeyUI != null) sprintKeyUI.SetActive(true);

        // 4단계: 상호작용 키 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.DialogueNShowInteractKey);
        if (sprintKeyUI != null) sprintKeyUI.SetActive(false);
        if (interactKeyUI != null) interactKeyUI.SetActive(true);

        // 5단계: 무기 선택방 진입
        yield return WaitForTrigger(TutorialTriggerType.WeaponRoom);
        if (interactKeyUI != null) interactKeyUI.SetActive(false);

        // 문 자동 닫기 + 플레이어 천천히 멈추기
        if (weaponRoomDoor != null) weaponRoomDoor.ForceClose();
        var player = PlayerStateMachine.Instance;
        var rb = player.Rb;

        player.SetMovementLocked(true);
        rb.linearVelocity = Vector3.zero;

        if (playerStopPoint != null)
        {
            Vector3 target = new Vector3(playerStopPoint.position.x, rb.position.y, rb.position.z);
            yield return rb.DOMove(target, 1.5f).SetEase(Ease.InOutSine).WaitForCompletion();
        }
        else
            yield return new WaitForSecondsRealtime(1.5f);

        player.ChangeState<Idle>();
        yield return new WaitForSecondsRealtime(0.3f);

        // 카드 표시 + 시간 정지
        if (weaponCardContainer != null) weaponCardContainer.SetActive(true);
        Time.timeScale = 0f;

        // 카드 선택 대기 (SelectWeapon에서 timeScale 1로 복구 + OnTrigger 호출)
        yield return WaitForTrigger(TutorialTriggerType.WeaponSelected);
        PlayerStateMachine.Instance.SetMovementLocked(false);

        // 무기 선택 후 장착하면서 전투시작

        // 6단계: 전투 시작
        yield return WaitForTrigger(TutorialTriggerType.CombatStart);
        if (weaponRoomDoor != null) weaponRoomDoor.Break();
        if (tutorialWolf != null)
        {
            tutorialWolf.SetTarget(PlayerStateMachine.Instance);
            tutorialWolf.ChangeState<MonsterStates.Chase>();
        }

        // 7단계: 공격 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.AttackTutorial);
        // TODO: 공격 키 UI 표시
        yield return WaitForInput("Attack");
        // TODO: 공격 키 UI 숨김

        // 8단계: 회피 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.DodgeTutorial);
        // TODO: 회피 키 UI 표시
        yield return WaitForInput("Dodge");
        // TODO: 회피 키 UI 숨김

        // 9단계: 패링 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.ParryTutorial);
        yield return Phase_ParryTutorial();

        // 10단계: 웅크리기 튜토리얼 및 대사
        yield return WaitForTrigger(TutorialTriggerType.DialogeNShowCrunchKey);
        // TODO: 대사 출력, 웅크리기 키 UI 표시

        // 튜토리얼 종료
        EndTutorial();
    }

IEnumerator Phase_ParryTutorial()
    {
        Time.timeScale = 0f;
        // TODO: 패링 키 UI 표시
        yield return WaitForInput("Guard");
        // TODO: 패링 키 UI 숨김
        Time.timeScale = 1f;
    }

    void EndTutorial()
    {
        // TODO: 튜토리얼 종료 처리
        Debug.Log("튜토리얼 완료");
    }
}
