using UnityEngine;
[System.Serializable]
public class PlayerStatus
{
    [Header("Default Stat")]
    public int hp;
    public string name;
    public string stamina;
    public float speed;
    public float crunchspeed;
    public float sprintspeed;

    [Header("Player Status")]
    public float parryduration;
    public float hungry;
    public float water;
    [Header("Stamina Cost")]
    public float parryCost;
    public float DodgeCost;
    public float attckCost;
}
