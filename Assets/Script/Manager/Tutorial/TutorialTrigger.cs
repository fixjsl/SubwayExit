using UnityEngine;
public enum TutorialTriggerType
{
    ShowMoveKeyNdialogue,//게임 시작시 첫 대사 및 이동키 튜토리얼
    WolfFearNdialogue,//늑대가 플레이어 발견하고 쫓아옴, 그리고 대사
    ShowSprintKey,//달리기 키 튜토리얼
    DialogueNShowInteractKey,//대사 및 상호작용키 튜토리얼
    WeaponRoom,// 무기 선택카드 및 문 닫힘
    WeaponSelected,// 무기 선택 완료
    CombatStart,// 적이 문 부수고 나옴 및 전투 튜토리얼 시작
    AttackTutorial, // 공격 키 튜토리얼
    DodgeTutorial, // 회피 키 튜토리얼
    ParryTutorial, // 패링 키 튜토리얼
    DialogeNShowCrunchKey, // 대사 및 웅크리기 키 튜토리얼
}



public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private TutorialTriggerType triggerType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != Layercache.Player) return;
        TutorialManager.Instance.OnTrigger(triggerType);
        gameObject.SetActive(false);
    }
}