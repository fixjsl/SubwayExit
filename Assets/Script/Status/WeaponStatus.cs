using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "Scriptable Objects/WeaponStatus")]
public class WeaponStatus : ScriptableObject
{
    [Header("기본 스탯")]
    public int attack;
    public float attackSpeed;
    public float attackRange;
    public float execution_m;
    [Header("특수 효과")]
    public int bloodStrenth;
    public float stunStrength;
    public float GuardStrength;

    [Header("스태미나 소모")]
    public int attackStamina;
    public int guardStamina;
    public int parryStamina;

    [Header("애니메이션")]
    public AnimatorOverrideController WeaponAnimations; // 무기별 모션

    [Header("공격히트박스")]
    public Collider attackcolider;

    public Vector3 holdPosition; // 잡히는 위치
    public Quaternion holdRotation; // 회전값
}
