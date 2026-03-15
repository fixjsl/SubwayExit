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
    [Header("Player Hidden")]
    public float currentnoise;
    public float currentbrighten;
    [Header("Noise")]
    [SerializeField] private float _noiseIdle;
    [SerializeField] private float _noiseCrouch;
    [SerializeField] private float _noiseWalk;
    [SerializeField] private float _noiseSprint;
    public float noiseIdle
    {
        get => _noiseIdle;
        set { _noiseIdle = value; ChangeNoiseStat?.Invoke(); }
    }
    public float noiseCrouch
    {
        get => _noiseCrouch;
        set { _noiseCrouch = value; ChangeNoiseStat?.Invoke(); }
    }
    public float noiseWalk
    {
        get => _noiseWalk;
        set { _noiseWalk = value; ChangeNoiseStat?.Invoke(); }
    }
    public float noiseSprint
    {
        get => _noiseSprint;
        set { _noiseSprint = value; ChangeNoiseStat?.Invoke(); }
    }
    [Header("Recovery")]
    public float staminaRecoverey;

    public float dodgeSpeed;
    //Event
    //base Stat event
    public event Action<float> ChangeHP;
    public event Action OnDie;
    public event Action<float> ChangeStamina;
    public event Action StaminaEmpty;
    //Noise stat event
    public event Action ChangeNoiseStat;
    public void UseStamina(float amount)
    {
        Stamina -= amount; // 그냥 차감, Stamina 프로퍼티에서 Clamp + 이벤트 처리
    }
}
