using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

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
    

    [SerializeField] Transform weaponPos;
    GameObject currentWeaponUsed;

    float moveX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerHealth = GetComponent<Health>();
        playerInventory = GetComponent<Inventory>();

    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveX * speed;

        if (waitForCanJump) 
        { //jump buffer
            if(isGrounded) OnJump();
        }
    }

    private void Update()
    {
        
    }

    public void OnMove(InputValue val)
    {
        moveX = val.Get<Vector2>().x;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") && collision.transform.position.y < transform.position.y)
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
