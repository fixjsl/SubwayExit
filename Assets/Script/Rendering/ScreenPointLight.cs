using UnityEngine;

public class ScreenPointLight : MonoBehaviour
{
    [SerializeField] private float range = 0.15f;
    [SerializeField] private float intensity = 1f;
    [SerializeField] private Color color = new Color(1f, 0.55f, 0.1f);

    public float Range => range;
    public float Intensity => intensity;
    public Color LightColor => color;

    void OnEnable()
    {
        if (ScreenPointLightManager.Instance != null)
            ScreenPointLightManager.Instance.Register(this);
    }

    void Start()
    {
        // OnEnable이 Manager보다 먼저 실행됐을 경우 여기서 재등록
        if (ScreenPointLightManager.Instance != null && isActiveAndEnabled)
            ScreenPointLightManager.Instance.Register(this);
    }

    void OnDisable()
    {
        if (ScreenPointLightManager.Instance != null)
            ScreenPointLightManager.Instance.Unregister(this);
    }
}
