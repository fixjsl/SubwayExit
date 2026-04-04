using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{

[SerializeField] private GameObject WeaponP;
[SerializeField] private Transform WeaponSpawnPoint;
[SerializeField] private Button WeaponSelectButton;

    void Awake()
    {
        WeaponSelectButton.onClick.AddListener(() => spawnWeapon(WeaponP));
    }

    public void spawnWeapon(GameObject weapon)
    {
        GameObject sweapon = Instantiate(weapon, WeaponSpawnPoint.position, WeaponSpawnPoint.rotation);
        WeaponSpawnPoint.GetComponent<WeaponPickup>().SetWeapon(sweapon.GetComponent<Weapon>());
    }


}
