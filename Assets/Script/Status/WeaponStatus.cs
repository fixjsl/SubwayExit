using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "Scriptable Objects/WeaponStatus")]
public class WeaponStatus : ScriptableObject
{
    [Header("기본 스탯")]
    public int attack;
    public float attackSpeed;
    public float attackRange;

    [Header("특수 효과")]
    public int bloodStrenth;
    public float stunStrength;
    public float GuardStrength;

    [Header("스태미나 소모")]
    public int attackStamina;
    public int guardStamina;
    public int parryStamina;

    [Header("애니메이션")]
    public string[] attackAnimations; // 무기별 공격 모션
}
