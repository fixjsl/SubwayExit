using UnityEngine;

public class WeaponPickup : ItObjectBase
{
    private Weapon weapon;

    public override bool isStuck => false;

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    protected override void OnInteractInternal(Vector3 interacterPosition)
    {
        if (weapon == null) return;
        Debug.Log(weapon);
        PlayerStateMachine.Instance.EquipWeapon(weapon);
        GetComponent<Collider>().enabled = false;
    }
}
