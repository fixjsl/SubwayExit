using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    private MonsterStateMachine monster;

    private void Awake()
    {
        monster = GetComponentInParent<MonsterStateMachine>();
        GetComponent<Collider>().enabled = false;
    }

    public void OnAttackCollider() => GetComponent<Collider>().enabled = true;
    public void OffAttackCollider() => GetComponent<Collider>().enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponentInParent<PlayerStateMachine>();
        if (player == null) return;
        if(player.isParrying)
        {
            monster.ChangeStun();
            return;
        }
        player.OnHit(monster.status.atk);
    }
}
