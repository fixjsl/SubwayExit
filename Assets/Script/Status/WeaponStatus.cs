using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "Scriptable Objects/WeaponStatus")]
public class WeaponStatus : ScriptableObject
{
    [Header("�⺻ ����")]
    public int attack;
    public float attackSpeed;
    public float attackRange;
    public float executionRange;
    public float execution_m;
    [Header("Ư�� ȿ��")]
    public int bloodStrenth;
    public float stunStrength;
    public float GuardStrength;

    [Header("���¹̳� �Ҹ�")]
    public int attackStamina;
    public int guardStamina;
    public int parryStamina;

    [Header("�ִϸ��̼�")]
    public AnimatorOverrideController WeaponAnimations; // ���⺰ ���

    public Vector3 holdPosition; // ������ ��ġ
    public Quaternion holdRotation; // ȸ����
}
