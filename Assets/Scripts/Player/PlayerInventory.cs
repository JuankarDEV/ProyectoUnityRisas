using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
     public List<GameObject> weapons = new List<GameObject>();
    public Transform weaponHolder; // el punto donde se colocará el arma

    private GameObject currentWeapon;

    public void AddWeapon(GameObject weaponPrefab)
    {
        if (!weapons.Contains(weaponPrefab))
        {
            weapons.Add(weaponPrefab);
            EquipWeapon(weaponPrefab); // equipar al instante
        }
    }

   public void EquipWeapon(GameObject weaponPrefab)
{
    if (!weapons.Contains(weaponPrefab))
        return;

    // Destruir arma anterior si hay
    if (currentWeapon != null)
        Destroy(currentWeapon);

    // Instanciar nueva arma como hijo del weaponHolder
    currentWeapon = Instantiate(weaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
    currentWeapon.transform.localPosition = Vector3.zero;
    currentWeapon.transform.localRotation = Quaternion.identity;

    // Activar WeaponLogic solo en la instancia
    WeaponLogic logic = currentWeapon.GetComponent<WeaponLogic>();
    if(logic != null)
    {
        logic.spawnPoint = weaponHolder; // asignar el holder
        logic.enabled = true;       // activar script
    }
}

}
