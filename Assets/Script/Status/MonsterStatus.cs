using System;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMonsterStatus", menuName = "MonsterStatus")]
public class MonsterStatus : ScriptableObject
{
    public float Maxhp;
    private float hp;
    public float Hp
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, Maxhp);
            ChangeHP?.Invoke(hp);
            if (hp <= 0) OnDie?.Invoke();
        }
    }
    public float speed;
    public float chasespeed;
    public float atk;
    public float atk_range;
    public float atkdelay;

    public float detect_range;
    public float detection_gauge;
    public float recovery;

    public float battle_range;
    public float stunTime;

    public event Action<float> ChangeHP;
    public event Action OnDie;
}