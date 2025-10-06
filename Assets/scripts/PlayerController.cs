using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    Health playerHealth;
    Inventory playerInventory;
    [SerializeField] float speed;
    [SerializeField] float jumpDampMult;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpBufferTime;
    private bool isJumping;
    private bool isGrounded;
    private bool waitForCanJump = false;

    [SerializeField] LayerMask interactableLayer;
    [SerializeField] float pickupRange = 2f;
    

    [SerializeField] Transform weaponPos;
    GameObject currentWeaponUsed;
    GameObject currentWeaponPrefab;

    float moveX;
    public bool facingRight = true;

    public float deathDelay = 0.5f;
    public bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<Health>();
        playerInventory = GetComponent<Inventory>();

    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rb.linearVelocityX = moveX * speed;

            if (waitForCanJump) 
            { //jump buffer
                if(isGrounded) OnJump();
            }
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            HandleCurrHeldWeapon();
            CheckHealth();
        }
    }

    private void CheckHealth()
    {
        if (playerHealth.GetHealth() <= 0)
        {
            isDead = true;
            StartCoroutine(ReloadOnDeathDelay());
        }
    }

    private IEnumerator ReloadOnDeathDelay()
    {
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnInteract()
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, pickupRange, interactableLayer);
        foreach (var overlap in overlaps)
        {
            InteractableObject item = overlap.GetComponent<InteractableObject>();
            if (item != null && item.GetPickUpState())
            {
                item.PickUpObj();
                break;
            }
        }
    }

    public void OnAttack()
    {
        // you have to actually have a weapon in hand
        if (currentWeaponUsed != null)
        {
            Weapon weaponControl = currentWeaponUsed.GetComponent<Weapon>();
            if (weaponControl != null && weaponControl.melee)
            {
                weaponControl.MeleeAttack();
            }
        }
    }

    public void OnMove(InputValue val)
    {
        moveX = val.Get<Vector2>().x;
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public void OnJump()
    {
        if (isGrounded && !isJumping)
        {
            isGrounded = false;
            isJumping = true;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (!isGrounded)
        { //checks if you touch the ground in the next .1 seconds and initiates the jump then
            waitForCanJump = true;
            StartCoroutine("JumpBuffer");
        }

    }

    public void OnJumpRelease()
    {
        if (rb.linearVelocityY > 0 && isJumping)
        {
            rb.linearVelocityY *= jumpDampMult;
        }
    }

    void HandleCurrHeldWeapon()
    {
        if (playerInventory.itemHeld != null && currentWeaponUsed == null)
        {
            currentWeaponUsed = Instantiate(playerInventory.itemHeld, weaponPos.position, Quaternion.identity, weaponPos.transform);
            currentWeaponUsed.SetActive(true);
            currentWeaponPrefab = playerInventory.itemHeld;
        }
        // else if (playerInventory.itemHeld != null && playerInventory.itemHeld != currentWeaponUsed)
        else if (playerInventory.itemHeld != null && playerInventory.itemHeld != currentWeaponPrefab)
        {
            Destroy(currentWeaponUsed);
            currentWeaponUsed = Instantiate(playerInventory.itemHeld, weaponPos.position, Quaternion.identity, weaponPos.transform);
            currentWeaponUsed.SetActive(true);
            currentWeaponPrefab = playerInventory.itemHeld;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }

    private IEnumerator JumpBuffer() 
    {

        yield return new WaitForSeconds(jumpBufferTime);
        waitForCanJump = false;
    }

}
