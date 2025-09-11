using UnityEngine;
using UnityEngine.Rendering.Universal;
public class PickupWeapon : MonoBehaviour
{
    public GameObject weapon;
    public Light2D pickupLight;
    void Start()
    {
            
            WeaponLogic logic = weapon.GetComponent<WeaponLogic>();
            if(logic != null)
                logic.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }
    
   private void OnTriggerEnter2D(Collider2D other)
{
    if(other.CompareTag("Player"))
    {
        // Añadir arma al inventario (instancia nueva)
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if(inventory != null)
            inventory.AddWeapon(weapon); // aquí se instancia en la mano

        // Apagar luz y desactivar arma del mundo
        if(pickupLight != null)
            pickupLight.enabled = false;

        // Opcional: desactivar solo visual del arma en el mundo
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}

}
