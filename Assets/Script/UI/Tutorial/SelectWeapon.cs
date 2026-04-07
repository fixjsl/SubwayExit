using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour
{

    [SerializeField] private GameObject WeaponP;
    [SerializeField] private Transform WeaponSpawnPoint;
    [SerializeField] private GameObject WeaponCardContainer;
    private WeaponPickup weaponPickup;
    [SerializeField] private Button WeaponSelectButton;

    void Awake()
    {
        WeaponSelectButton.onClick.AddListener(() => spawnWeapon(WeaponP));
        weaponPickup = WeaponSpawnPoint.GetComponent<WeaponPickup>();
    }

    public void spawnWeapon(GameObject weapon)
    {
        GameObject sweapon = Instantiate(weapon, WeaponSpawnPoint.position, WeaponSpawnPoint.rotation);
        weaponPickup.SetWeapon(sweapon.GetComponent<Weapon>());
        TutorialManager.Instance.OnTrigger(TutorialTriggerType.WeaponSelected);
        if (WeaponCardContainer != null)
        {
            WeaponCardContainer.SetActive(false);
            
        }
    }
}
