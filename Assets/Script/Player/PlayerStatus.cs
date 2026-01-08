using System;
using UnityEngine;
[System.Serializable]
public class PlayerStatus
{
    [Header("Default Stat")]
    public string Name;
    public int Maxhp;
    private float hp;
    public float Hp { 
        get => hp;
        set {  
             if (hp <=0) return; 
             hp = Mathf.Clamp(value, 0, Maxhp);
            ChangeHP?.Invoke(hp);
             if (hp <= 0) OnDie?.Invoke();
        } 
    }
    public int MaxStamina;
    private float stamina;
    public float Stamina
    {
        get => stamina; 
        set
        {
            stamina = Mathf.Clamp(value, 0, MaxStamina);
           ChangeStamina?.Invoke(stamina);

            if (stamina <= 0) StaminaEmpty?.Invoke();
        }
    }
    public float currentspeed;
    public float walkspeed;
    public float crunchspeed;
    public float sprintspeed;

    [Header("Player Status")]
    public float parryduration;
    public float hungry;
    public float water;
    public float DodgeCooldown;
    [Header("Stamina Cost")]
    public float parryCost;
    public float DodgeCost;
    public float SprintCost;
    public float attckCost;


    public event Action<float> ChangeHP;
    public event Action OnDie;
    public event Action<float> ChangeStamina;
    public event Action StaminaEmpty;
}
