using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 80; // Maximum health of the enemy
    public int currentHealth; // Current health of the enemy

    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10; // Damage dealt to the player per attack

    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private float nextAttackTime;
    private BoxCollider2D boxCollider;
    // Ensure the player and enemy don't collide with each other
    private int playerLayerMask;

    void Start()
    {
        currentHealth = maxHealth; // Set initial health to max
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Add a BoxCollider component to the GameObject
        boxCollider = GetComponent<BoxCollider2D>();

        // Get the layer mask of the player
        playerLayerMask = LayerMask.GetMask("Player");
    }

    void Update()
    {
        // Check if the enemy is alive
        if (currentHealth <= 0)
        {
            Die();
            return; // Exit the Update method if the enemy is dead
        }
        
        Move();
        
    }


    void Move()
    {
        // Check if the player is in attack range
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // Attack the player
            if (Time.time >= nextAttackTime)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                    nextAttackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
        else
        {
            // Move towards the player
           
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            
            // Set animation parameters 
            animator.SetFloat("Horizontal", transform.position.x);
            animator.SetFloat("Vertical", transform.position.y);
            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
    }

    void Attack()
    {
        // Perform attack action here
        Debug.Log("Enemy attacks!");

        animator.SetBool("isAttacking", true);

        // Damage the player
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(attackDamage);
        }

    }

    // Method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by the damage amount
        Debug.Log("Enemy took damage. Now at " + currentHealth + " health.");
        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle enemy death
    void Die()
    {
        //add death animation here
        Debug.Log("Enemy defeated!");
        Destroy(gameObject); // Destroy the enemy GameObject
    }



    // Detect collisions with other colliders
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision with player detected!");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(boxCollider, collision.collider, true);
        }
    }

    // Ensure the player and enemy don't collide with each other
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(boxCollider, collision.collider, false);
        }
    }
}