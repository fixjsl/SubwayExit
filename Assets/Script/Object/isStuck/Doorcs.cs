

using UnityEngine;

public class Doorcs : ItObjectBase
{
    public override bool isStuck => false;

    protected override float interactCooldown => 0.5f;

    private Quaternion targetRot;
    private Quaternion closedRot;

    private bool isOpen = false;
    [SerializeField]
    private Collider triggerCollider; // 트리거 콜라이더 참조
    [SerializeField]
    private Transform offset; // 플레이어와의 상호작용 위치 오프셋

    private void Awake()
    {
        closedRot = transform.rotation;
        targetRot = closedRot;
        // 트리거 콜라이더 찾기 (자식이나 자신)
    }
    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        if (!isOpen)
        {
            Vector3 localPos = offset.transform.InverseTransformPoint(interacterPosition);
            float angle = localPos.x >= 0 ? 90f : -90f;
            Debug.Log($"Door interact: localPos.x={localPos.x}, angle={angle}");
            targetRot = closedRot * Quaternion.Euler(0f, angle, 0f);
            isOpen = true;
        }
        else
        {
            targetRot = closedRot;
            isOpen = false;
        }
    }
    private void Update()
    {
        if (offset.transform.rotation == targetRot) return;

        offset.transform.rotation = Quaternion.RotateTowards(
            offset.transform.rotation, targetRot, 180f * Time.deltaTime);

        // 트리거 콜라이더의 로컬 회전을 0으로 유지 (회전하지 않게)
        if (triggerCollider != null)
        {
            triggerCollider.transform.localRotation = Quaternion.identity;
        }

        if (offset.transform.rotation == targetRot)
            isInteracting = false;
    }

}

