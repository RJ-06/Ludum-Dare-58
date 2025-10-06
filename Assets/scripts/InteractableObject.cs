using System.ComponentModel;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    bool canPickUp = false;

    GameObject player = GameObject.FindGameObjectWithTag("Player");

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
        Inventory inventory = player.GetComponent<Inventory>();
        GameObject weapon = Instantiate(objectPrefab);
        inventory.AddObjectToInventory(weapon);
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
