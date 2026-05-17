using UnityEngine;

[RequireComponent(typeof(Light))]
public class Flashlight : MonoBehaviour
{
    [Header("콘 설정")]
    [SerializeField] private float outerAngle   = 60f;
    [SerializeField] private float innerAngle   = 25f;
    [SerializeField] private float range        = 8f;
    [SerializeField] private float intensity    = 2f;
    [SerializeField] private float tiltDown     = 30f;
    [SerializeField] private float heightOffset = 1.5f;
    [SerializeField] private float nearRange    = 1.8f;
    [SerializeField] private Color lightColor   = new Color(1f, 0.95f, 0.8f);
    [SerializeField] private Light spotLight;

    private float facingSign = 1f;

    void Start()
    {
        var player = PlayerStateMachine.Instance;
        if (player == null) return;
        player.EquipLight(spotLight);
        player.OnLightToggle += SetEnabled;
    }

    void OnDestroy()
    {
        if (PlayerStateMachine.Instance != null)
            PlayerStateMachine.Instance.OnLightToggle -= SetEnabled;
    }

    public void UpdateShaderGlobals()
    {
        bool isOn = spotLight != null && spotLight.enabled;
        Shader.SetGlobalFloat("_FlashlightEnabled", isOn ? 1f : 0f);

        float moveInput = PlayerStateMachine.Instance.MoveInput;
        if (Mathf.Abs(moveInput) > 0.01f) facingSign = Mathf.Sign(moveInput);

        // 플레이어는 X축 이동 → 콘 방향을 XY 평면으로 계산
        Vector3 dir = new Vector3(
            facingSign * Mathf.Cos(tiltDown * Mathf.Deg2Rad),
            -Mathf.Sin(tiltDown * Mathf.Deg2Rad),
            0f
        ).normalized;
        Vector3 shaderPos =  spotLight.transform.position ;

        if (Time.frameCount % 90 == 0)
            Debug.Log($"[Flash] pos={shaderPos:F2}  dir={dir:F2}  range={range}  facing={facingSign}");

        Shader.SetGlobalVector("_FlashlightPos",        shaderPos);
        Shader.SetGlobalVector("_FlashlightDir",        dir);
        Shader.SetGlobalFloat ("_FlashlightRange",      range);
        Shader.SetGlobalFloat ("_FlashlightOuterAngle", Mathf.Cos(outerAngle * Mathf.Deg2Rad));
        Shader.SetGlobalFloat ("_FlashlightInnerAngle", Mathf.Cos(innerAngle * Mathf.Deg2Rad));
        Shader.SetGlobalFloat ("_FlashlightIntensity",  intensity);
        Shader.SetGlobalColor ("_FlashlightColor",      lightColor);
        Shader.SetGlobalFloat ("_FlashlightNearRange",  nearRange);
    }

    private void SetEnabled(bool on)
    {
        if (spotLight != null) spotLight.enabled = on;
        Shader.SetGlobalFloat("_FlashlightEnabled", on ? 1f : 0f);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Transform player = PlayerStateMachine.Instance != null
            ? PlayerStateMachine.Instance.transform
            : transform.parent;
        if (player == null) return;

        Vector3 dir      = Quaternion.AngleAxis(-tiltDown, player.right) * Vector3.down;
        Vector3 pos      = spotLight.transform.position ;
        float   cosOuter = Mathf.Cos(outerAngle * Mathf.Deg2Rad);

        // 광원 위치
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(pos, 0.15f);

        // 중심 방향
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(pos, dir.normalized * range);

        // 콘 외곽선 (8개 방향)
        Gizmos.color = new Color(0f, 0.8f, 0.8f, 0.6f);
        Vector3 perp = Vector3.Cross(dir.normalized, player.up).normalized;
        if (perp.sqrMagnitude < 0.01f)
            perp = Vector3.Cross(dir.normalized, player.right).normalized;

        int segments = 12;
        for (int i = 0; i < segments; i++)
        {
            float   angle    = i * (360f / segments);
            Vector3 rotPerp  = Quaternion.AngleAxis(angle, dir.normalized) * perp;
            Vector3 edgeDir  = (dir.normalized * cosOuter + rotPerp * Mathf.Sqrt(1f - cosOuter * cosOuter)).normalized;
            Gizmos.DrawRay(pos, edgeDir * range);
        }

        // nearRange 구체
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawWireSphere(pos, nearRange);
    }
#endif
}
