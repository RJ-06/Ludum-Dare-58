using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("NOTE - this is the script for weapons you use, not the object of the weapon that you can pick up")]

    [Tooltip("Used for weapon durability")]
    public Health health;
    [SerializeField] int damagePerHit; //TODO - make this all scriptable object based - derived classes are SOs?


    private int placeInInventory = -1;
    void Attack()
    {
        //IMPLEMENT - inheritance based? different attack based on type of thing?
    }

    void ThrowWeapon(Vector2 throwDir, float throwForce)
    {
        //TODO - remember remove from inventory
    }

    void SetPlaceInInventory(int newPlaceInInventory) 
    { //to use when shifting, getting what object to remove
        placeInInventory = newPlaceInInventory;
    }

    int GetPlaceInInventory() 
    {
        return placeInInventory;
    }
}
