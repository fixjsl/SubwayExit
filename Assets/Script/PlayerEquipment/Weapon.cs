using UnityEngine;
using System.Collections.Generic;
public class Weapon : MonoBehaviour, ICreatable
{
    private PlayerStateMachine player;
    [SerializeField]
    public WeaponStatus status;
    [SerializeField] private Collider weaponCollider;
    [SerializeField] private ItemBase _iteminfo;
    public ItemBase iteminfo => _iteminfo;
    public Transform secondaryGrip;
    private HashSet<MonsterStateMachine> hitTargets = new HashSet<MonsterStateMachine>();


    private void Awake()
    {
        status = Instantiate(status);
        weaponCollider.enabled = false;
    }

    public void SetPlayer(PlayerStateMachine stateMachine)
    {
        player = stateMachine;
    }

    public void OnAttackColider() 
    {
    hitTargets.Clear(); // 공격 시작 시 초기화
    weaponCollider.enabled = true;
    }
    public void OffAttackColider() => weaponCollider.enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        var monster = other.GetComponentInParent<MonsterStateMachine>();
        if (monster == null) return;
        if (!hitTargets.Add(monster)) return; // 이미 맞은 대상이면 스킵
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
            int key = material.item.iteminfo.itemcode;
            if (!inventory.slots.TryGetValue(key, out int count)) return false;
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
