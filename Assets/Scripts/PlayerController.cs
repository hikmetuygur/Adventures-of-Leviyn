using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpSpeed = 20f;

    private Rigidbody2D rigidbody;
    private Animator animator;
    
    Vector2 moveInput;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        Run();
        FlipSprite();
        
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
        

        if (value.isPressed)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpSpeed);
        }
    }
}
