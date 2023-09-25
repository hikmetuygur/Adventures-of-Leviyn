using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpSpeed = 25f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float gravityScaleAtStart;

    private Rigidbody2D rigidbody;
    private Animator animator;
    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetCollider2D;
    
    
    Vector2 moveInput;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody.gravityScale;
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 moveVelocity = new Vector2(moveInput.x * playerSpeed, rigidbody.velocity.y);
        rigidbody.velocity = moveVelocity;
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning",false);
        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), 1f);
            animator.SetBool("isRunning", true);
        }
        
    }
    void OnJump(InputValue value)
    {
        if (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (value.isPressed)
        {
            rigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
        
    }

    void ClimbLadder()
    {
        if (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            animator.SetBool("isClimbing", false);
            rigidbody.gravityScale = gravityScaleAtStart;
            return;
        }

        rigidbody.gravityScale = 0;
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, moveInput.y * climbSpeed);
        rigidbody.velocity = climbVelocity;
        bool hasVerticalSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", hasVerticalSpeed);
    }
}
