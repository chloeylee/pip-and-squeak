using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSqueak : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 13f;
    private bool isFacingRight = true;
    private bool crouchFlag = false;

    [SerializeField] public Collider2D standingCollider, crouchingCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform overheadCheckCollider;
    [SerializeField] private float overheadCheckRadius = 0.2f;

    // Hardcoded input keys
    private KeyCode moveLeftKey = KeyCode.A;   // Left movement key
    private KeyCode moveRightKey = KeyCode.D;  // Right movement key
    private KeyCode jumpKey = KeyCode.W;       // Jump key
    private KeyCode crouchKey = KeyCode.S;     // Crouch key

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        crouchFlag = false;
        standingCollider.enabled = true;
        crouchingCollider.enabled = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: split this up into helpers
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(moveLeftKey))
        {
            horizontal = -1f; // move left
            animator.SetBool("isWalking", true);
        }
        if (Input.GetKey(moveRightKey))
        {
            horizontal = 1f; // move right
            animator.SetBool("isWalking", true);
        }
        if (!Input.GetKey(moveLeftKey) && !Input.GetKey(moveRightKey))
        {
            horizontal = 0f; // stop moving
            animator.SetBool("isWalking", false);
        }

        // Jumping logic
        if (Input.GetKey(jumpKey) && IsGrounded())
        {
            animator.SetBool("isJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

        }
        else
        {
            animator.SetBool("isJumping", false);
        }

        // Crouching logic
        // TODO: change anim if crouching vs crouch walking
        if (Input.GetKeyDown(crouchKey) && !crouchFlag) // Start crouching
        {
            crouchFlag = true;
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetKeyUp(crouchKey) && crouchFlag) // Stop crouching
        {
            // Check if there's enough space to stand up
            if (!Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                crouchFlag = false;
                animator.SetBool("isCrouching", false);
            }
        }

        // Update colliders based on crouch state
        standingCollider.enabled = !crouchFlag;
        crouchingCollider.enabled = crouchFlag;


        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cobweb")
        {
            speed = speed / 3;
            jumpingPower = jumpingPower / 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cobweb")
        {
            speed = 8f;
            jumpingPower = 13f;
        }
    }
}
