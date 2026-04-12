using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private Image hpFill;

    [Header("Stamina")]
    [SerializeField] private Image staminaFillLeft;
    [SerializeField] private Image staminaFillRight;

    [Header("Hungry")]
    [SerializeField] private Image hungryFill;

    [Header("Water")]
    [SerializeField] private Image waterFill;

    [SerializeField] private TextMeshPro HourMinute;
    [SerializeField] private TextMeshPro Day;


    private PlayerStatus status;

    void Start()
    {
        status = PlayerStateMachine.Instance.status;

        status.ChangeHP       += OnHPChanged;
        status.ChangeStamina  += OnStaminaChanged;
        status.ChangeHungry   += OnHungryChanged;
        status.ChangeWater    += OnWaterChanged;
        GameManager.Instance.ChangeTime += OnChangeTime;
        GameManager.Instance.ChangeDay += OnChangeDay;

        // 초기값 반영
        SetFill(hpFill,      status.Hp,      status.Maxhp);
        SetStaminaFill(status.Stamina, status.curMaxStamina);
        SetFill(hungryFill,  status.Hungry,   status.MaxHungry);
        SetFill(waterFill,   status.Water,    status.MaxWater);
        HourMinute.text = $"{GameManager.Instance.Hour:D2}:{GameManager.Instance.Minute:D2}";
        Day.text = $"Day - {GameManager.Instance.Day}";
    }

    void OnHPChanged(float val)      => SetFill(hpFill,      val, status.Maxhp);
    void OnStaminaChanged(float val) => SetStaminaFill(val, status.curMaxStamina);
    void OnHungryChanged(int val)    => SetFill(hungryFill,  val, status.MaxHungry);
    void OnWaterChanged(int val)     => SetFill(waterFill,   val, status.MaxWater);

    void SetStaminaFill(float current, float max)
    {
        float ratio = current / max;
        if (staminaFillLeft)  staminaFillLeft.fillAmount  = ratio;
        if (staminaFillRight) staminaFillRight.fillAmount = ratio;
    }

    void SetFill(Image image, float current, float max)
    {
        if (image == null) return;
        image.fillAmount = current / max;
    }

    void OnDestroy()
    {
        if (status == null) return;
        status.ChangeHP      -= OnHPChanged;
        status.ChangeStamina -= OnStaminaChanged;
        status.ChangeHungry  -= OnHungryChanged;
        status.ChangeWater   -= OnWaterChanged;
        GameManager.Instance.ChangeTime -= OnChangeTime;
        GameManager.Instance.ChangeDay -= OnChangeDay;
    }

    void OnChangeTime(int hour, int minute)
    {
        HourMinute.text = $"{hour:D2}:{minute:D2}";
    }
    void OnChangeDay(int day, int hour)
    {
        Day.text = $"Day - {day}";
    }
}
