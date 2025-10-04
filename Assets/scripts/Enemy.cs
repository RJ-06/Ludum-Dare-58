using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Health health;
    [SerializeField] GameObject weaponDropped;

    //TODO SETUP - make some enums to keep track of things such as what type of attacker it is (melee vs ranged)
    //broaden this to work for having multiple attacks - bosses should work with this

    void Attack() 
    {
        //TODO - inheritance again yippee! different enemies inherit this class, implement with their own behaviour
    }

    void RangedAttack() 
    {
    
    }

    void MeleeAttack() 
    {
    
    }

}
