using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatus", menuName = "Scriptable Objects/WeaponStatus")]
public class WeaponStatus : ScriptableObject
{
    public int attack;
    public float attackSpeed;
    public float attackRange;
    public int bloodStrenth;
    public int stunStrength;

    public int attackStamina;
    public int guardStamina;
    public int parryStamina;
 }
