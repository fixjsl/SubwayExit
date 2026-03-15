using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    private MonsterStateMachine monster;

    private Collider hitbox;
    private void Awake()
    {
        monster = GetComponentInParent<MonsterStateMachine>();
        hitbox = monster.AttackCollider as Collider;
        hitbox.enabled = false;

    }

    public void OnAttackCollider() => hitbox.enabled = true;
    public void OffAttackCollider() => hitbox.enabled = false;

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
