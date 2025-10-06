using System.ComponentModel;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool canPickUp = false;

    [Tooltip("prefab of the weapon/object you grab")]
    [SerializeField] GameObject objectPrefab;

    //TODO - figure out shaders to do an outline when canPickUp is true, to indicate its grabbabble

    //
    // TODO - idk if it'd go on this script, but for weapons ie projectiles, make it so rigid body becomes static when they hit walls
    //


    public void PickUpObj()
    {
        //TODO - implement
        //make this disappear, add to inventory, particle effects
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Inventory inventory = player.GetComponent<Inventory>();
        GameObject weapon = Instantiate(objectPrefab);
        inventory.AddObjectToInventory(weapon);
        weapon.layer = 0;

        Transform weaponPos = player.transform.Find("WeaponPos");
        weapon.transform.SetParent(weaponPos);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        weapon.SetActive(false);
        Destroy(gameObject);
    }


    public bool GetPickUpState()
    {
        return canPickUp;
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            canPickUp = false;
        }
    }
}
