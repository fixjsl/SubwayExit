using UnityEngine;

public class Weapon : MonoBehaviour, ICreatable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerStateMachine player;
    [SerializeField]
    public WeaponStatus status;
    private Collider weaponCollider;
    [SerializeField] private ItemBase _iteminfo;
    public ItemBase iteminfo => _iteminfo;



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

    public bool canCraft(Inventory inventory)
    {
        foreach (var material in iteminfo.materials)
        {
            if (!inventory.slots.TryGetValue(material.item, out int count)) return false;
            if (count < material.amount) return false;
        }
        return true;
    }

    public void Craft(Inventory inventory)
    {
        if (!canCraft(inventory)) return;

        foreach (var material in iteminfo.materials)
            inventory.RemoveItem(material.item, material.amount);
        player.EquipWeapon(this);
    }
}
