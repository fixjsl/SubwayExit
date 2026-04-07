using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target; // 따라다닐 대상 (플레이어)

    [Header("Position Settings")]
    public Vector3 offset  = new  Vector3(0,14,-65);// 카메라 위치 오프셋
    public float smoothSpeed = 0.125f; // 따라가는 부드러움 (0~1, 높을수록 빠름)

    [Header("Rotation Settings")]
    public bool lookAtTarget = true; // 대상 바라보기 여부
    public float rotationSpeed = 5f; // 회전 속도

    [Header("Auto Offset")]
    public bool useCurrentAsOffset = true; // 현재 위치를 오프셋으로 사용


    void LateUpdate()
    {
        if (target == null) return;

        // 목표 위치 계산 (x, y만 플레이어 따라감, z는 고정)
        Vector3 desiredPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z  // z는 현재 카메라 위치 유지
        );

        // 부드럽게 위치 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 대상 바라보기

    }

    // 런타임에 대상 설정
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}

