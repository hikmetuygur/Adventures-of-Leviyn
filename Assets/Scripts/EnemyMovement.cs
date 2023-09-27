using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    Rigidbody2D rigidbody2D;
    BoxCollider2D boxCollider2D;
    
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
    
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), 1f);
    }
}
