using UnityEngine;
[CreateAssetMenu(fileName = "NewMonsterStatus", menuName = "MonsterStatus")]
public class MonsterStatus : ScriptableObject
{
    
    public float hp;
    public float speed;
    public float atk;

    public float detect_range;
    public float detection_gauge;
    public float recovery;
}