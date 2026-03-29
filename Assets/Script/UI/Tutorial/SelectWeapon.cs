using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{

    public GameObject WeaponP{get; private set;}
    public Transform WeaponSpawnPoint;
    public Button WeaponSelectButton;

    void Awake()
    {
        WeaponSelectButton.onClick.AddListener(() => spawnWeapon(WeaponP));
    }

    public void spawnWeapon(GameObject weapon)
    {
        GameObject sweapon = Instantiate(weapon,WeaponSpawnPoint.position, WeaponSpawnPoint.rotation);
        sweapon.SetActive(true);
    }


}
