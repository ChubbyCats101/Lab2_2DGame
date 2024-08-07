using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private GatherInput gatherInput;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;

    private int direction = 1; // to right-hand side

    public float jumpForce;

    public float rayLength;
    public LayerMask groundLayer;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool grounded = false;
    private int jumpCount = 0; // Add jumpCount

    // Start is called before the first frame update
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SetAnimatorValues();
    }
    private void FixedUpdate()
    {
        CheckStatus();
        Move();
        JumpPlayer();
    }
    private void Move()
    {
        Flip();
        Rigidbody2D.velocity = new Vector2(speed * gatherInput.valueX, Rigidbody2D.velocity.y);
    }
    private void Flip()
    {
        if (gatherInput.valueX * direction < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            direction *= -1;
        }
    }
    private void SetAnimatorValues()
    {
        animator.SetFloat("Speed", Mathf.Abs(Rigidbody2D.velocity.x));
        animator.SetFloat("vSpeed", Rigidbody2D.velocity.y);
        animator.SetBool("Grounded", grounded);
    }
    private void JumpPlayer()
    {
        if (gatherInput.jumpInput && jumpCount < 2) // Allow jump if jumpCount is less than 2
        {
            Rigidbody2D.velocity = new Vector2(gatherInput.valueX * speed, jumpForce);
            jumpCount++; // Increment jumpCount
        }
        gatherInput.jumpInput = false;
    }
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightPoint.position, Vector2.down, rayLength, groundLayer);
        grounded = leftCheckHit || rightCheckHit; // Consider grounded if either check is true

        if (grounded)
        {
            jumpCount = 0; // Reset jumpCount when grounded
        }
    }
}
