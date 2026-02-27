using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerStateMachine player;
    [SerializeField]
    public WeaponStatus status;
    private Collider weaponCollider;
    


    private void Awake()
    {
        player = GetComponentInParent<PlayerStateMachine>();
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }

    public void OnAttackColider() =>         weaponCollider.enabled = true;
    public void OffAttackColider() =>         weaponCollider.enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        var monster = other.GetComponentInParent<MonsterStateMachine>();
        if (monster == null) return;
        OnHit(monster);
    }

    public virtual void OnHit(MonsterStateMachine monster)
    {
        monster.OnHit(status.attack, status.stunStrength);
    }
}
