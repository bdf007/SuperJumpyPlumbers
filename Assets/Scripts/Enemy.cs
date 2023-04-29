using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D enemyRb;
    [SerializeField]private float speed;
    private Vector2 movementDirection;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        movementDirection = Vector2.right;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(movementDirection);
        
    }


    // move the enemy in a direction based on the speed
    public void Move(Vector2 direction)
    {
        movementDirection = direction;
        enemyRb.velocity = new Vector2 (movementDirection.x * speed, enemyRb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            movementDirection *= -1f;
        }
    }
}
