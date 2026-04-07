using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    private TutorialTriggerType? pendingTrigger = null;

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
            yield return null;
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
        // 1단계: 이동키 튜토리얼 및 첫 대사
        yield return WaitForTrigger(TutorialTriggerType.ShowMoveKeyNdialogue);
        // TODO: 대사 출력, 이동키 UI 표시

        // 2단계: 늑대 등장 및 대사
        yield return WaitForTrigger(TutorialTriggerType.WolfFearNdialogue);
        // TODO: 늑대 추격 시작, 대사 출력

        // 3단계: 달리기 키 튜토리얼
        yield return WaitForTrigger(TutorialTriggerType.ShowSprintKey);
        // TODO: 달리기 키 UI 표시

        // 4단계: 상호작용 키 튜토리얼 및 대사
        yield return WaitForTrigger(TutorialTriggerType.DialogueNShowInteractKey);
        // TODO: 대사 출력, 상호작용 키 UI 표시

        // 5단계: 무기 선택방
        yield return WaitForTrigger(TutorialTriggerType.WeaponRoom);
        // TODO: 무기 선택 UI 표시, 문 닫기
        yield return WaitForTrigger(TutorialTriggerType.WeaponSelected);
        // TODO: 문 열기

        // 6단계: 전투 시작
        yield return WaitForTrigger(TutorialTriggerType.CombatStart);
        // TODO: 문 부수기 연출, 몬스터 등장

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
