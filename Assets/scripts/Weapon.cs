using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("NOTE - this is the script for weapons you use, not the object of the weapon that you can pick up")]

    [Tooltip("Used for weapon durability")]
    public Health health;
    [SerializeField] int damagePerHit; 

    public bool melee = true;

    public bool attacking = false;
    public bool beingThrown = false;
    [SerializeField] Transform rotateAroundPoint;
    [SerializeField] float meleeRotateAngle;
    private Rigidbody2D rb;

    private int placeInInventory = -1;

    [SerializeField] string opponentTag;

    [SerializeField] GameObject weaponPickup;

    void Attack()
    {
        //IMPLEMENT - inheritance based? different attack based on type of thing?
    }

    void ThrowWeapon(Vector2 throwDir, float throwForce)
    {
        rb.AddForce(throwDir * throwForce, ForceMode2D.Impulse);
        beingThrown = true;
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

    public void MeleeAttack()
    {
        attacking = true;
        StartCoroutine(Rotation());
    }

    private IEnumerator Rotation()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject player = FindAnyObjectByType<PlayerController>().gameObject;

        if (player.GetComponent<PlayerController>().facingRight)
        {
            transform.RotateAround(rotateAroundPoint.position, new Vector3(0, 0, 1), -30f);
            yield return new WaitForSeconds(0.1f);
            transform.RotateAround(rotateAroundPoint.position, new Vector3(0, 0, 1), 30f);
        }
        else
        {
            transform.RotateAround(rotateAroundPoint.position, new Vector3(0, 0, 1), 30f);
            yield return new WaitForSeconds(0.1f);
            transform.RotateAround(rotateAroundPoint.position, new Vector3(0, 0, 1), -30f);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacking && collision.CompareTag(opponentTag))
        {
            Health oppHealth = collision.gameObject.GetComponent<Health>();
            oppHealth.TakeDamage(damagePerHit);
        }
        else if (beingThrown && collision.CompareTag(opponentTag))
        {
            Health oppHealth = collision.gameObject.GetComponent<Health>();
            oppHealth.TakeDamage(damagePerHit);
        }
        else if (beingThrown && (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("wall")))
        {
            StartCoroutine("stickInWall");
        }
    }

    IEnumerator stickInWall() 
    {
        yield return new WaitForSeconds(.1f);
        rb.bodyType = RigidbodyType2D.Static;

        GameObject g = Instantiate(weaponPickup, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
