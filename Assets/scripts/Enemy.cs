using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Properties")]
    protected string name;
    public Health health;
    public float detectRadius;
    protected LayerMask playerLayer;
    protected Transform target;
    
    [SerializeField] GameObject weaponDropped;

    protected virtual void Update()
    {
        DetectPlayer();
    }

    // written as if there is multiple players
    protected virtual void DetectPlayer()
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, detectRadius, playerLayer);
        if (overlaps.Length > 0)
        {
            // follows the first found
            target = overlaps[0].transform;
        }
        else
        {
            target = null;
        }
    }

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
