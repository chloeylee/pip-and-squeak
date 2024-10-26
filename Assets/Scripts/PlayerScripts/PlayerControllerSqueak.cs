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
    private KeyCode moveLeftKey = KeyCode.LeftArrow;   // Left movement key
    private KeyCode moveRightKey = KeyCode.RightArrow;  // Right movement key
    private KeyCode jumpKey = KeyCode.UpArrow;       // Jump key
    private KeyCode crouchKey = KeyCode.DownArrow;     // Crouch key

    // Start is called before the first frame update
    void Start()
    {
        crouchFlag = false;
        standingCollider.enabled = true;
        crouchingCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(moveLeftKey))
        {
            horizontal = -1f; // move left
        }
        if (Input.GetKey(moveRightKey))
        {
            horizontal = 1f; // move right
        }

        // Jumping logic
        if (Input.GetKey(jumpKey) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        // Crouching logic
        if (Input.GetKeyDown(crouchKey) && !crouchFlag) // Start crouching
        {
            crouchFlag = true;
        }
        else if (Input.GetKeyUp(crouchKey) && crouchFlag) // Stop crouching
        {
            // Check if there's enough space to stand up
            if (!Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer))
            {
                crouchFlag = false;
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
}
