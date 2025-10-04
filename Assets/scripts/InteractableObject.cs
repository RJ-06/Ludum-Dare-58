using System.ComponentModel;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    bool canPickUp = false;

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
    }


    public bool GetPickUpState() 
    {
        return canPickUp;
    }
}
