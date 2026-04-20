using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image hpFill;
    [SerializeField] private Image ghostFill;   // 데미지 표시용 (노랑/주황)
    [SerializeField] private float ghostDelay = 0.5f;   // 줄어들기 시작까지 대기
    [SerializeField] private float ghostSpeed = 2f;     // 줄어드는 속도

    [Header("Detection")]
    [SerializeField] private Image detectionFill;
    [SerializeField] private Image detect;

    private MonsterStateMachine monster;
    private Transform hpAnchor;
    private Transform detectionAnchor;

    private float ghostTimer;
    private bool ghostWaiting;

    public void Init(MonsterStateMachine target, Transform hpPoint, Transform detectionPoint)
    {
        monster = target;
        hpAnchor = hpPoint;
        detectionAnchor = detectionPoint;
        detect.gameObject.SetActive(false);

        // UICamera를 Canvas에 자동 연결
        var canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            Camera uiCam = GameObject.Find("UICamera")?.GetComponent<Camera>();
            if (uiCam != null) canvas.worldCamera = uiCam;
        }

        monster.status.ChangeHP += OnHPChanged;
        monster.status.ChangeDetectionGauge += (gauge) =>
        {
            if (detectionFill != null)
                detectionFill.fillAmount = gauge / 100f;
            if (gauge >= 100f)
            {
                detect.gameObject.SetActive(true);
            }
        };
        OnHPChanged(monster.status.Hp);
        detectionFill.fillAmount = 0f;
    }

    void Update()
    {
        if (monster == null) return;

        // 고스트 HP 처리
        if (ghostFill != null && ghostWaiting)
        {
            ghostTimer -= Time.deltaTime;
            if (ghostTimer <= 0f)
            {
                ghostWaiting = false;
                ghostFill.fillAmount = Mathf.MoveTowards(ghostFill.fillAmount, hpFill.fillAmount, ghostSpeed * Time.deltaTime);
            }
        }
        else if (ghostFill != null && ghostFill.fillAmount > hpFill.fillAmount)
        {
            ghostFill.fillAmount = Mathf.MoveTowards(ghostFill.fillAmount, hpFill.fillAmount, ghostSpeed * Time.deltaTime);
        }

        if (Camera.main != null)
        {
            Vector3 camForward = Camera.main.transform.forward;

            if (hpAnchor != null)
            {
                hpFill.transform.parent.position = hpAnchor.position;
            }
            if (detectionAnchor != null)
            {
                detectionFill.transform.parent.position = detectionAnchor.position;
            }
        }
    }

    void OnHPChanged(float hp)
    {
        if (hpFill == null) return;

        float newFill = hp / monster.status.Maxhp;

        // 데미지일 때만 고스트 활성화
        if (newFill < hpFill.fillAmount)
        {
            ghostWaiting = true;
            ghostTimer = ghostDelay;
        }

        hpFill.fillAmount = newFill;
    }

    void OnDestroy()
    {
        if (monster != null)
            monster.status.ChangeHP -= OnHPChanged;
    }
}
