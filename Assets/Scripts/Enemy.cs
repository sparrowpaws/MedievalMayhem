using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*
    public int maxHealth = 80; // Maximum health of the enemy
    public int currentHealth; // Current health of the enemy

    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10; // Damage dealt to the player per attack



    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private float nextAttackTime;


    void Start()
    {
        currentHealth = maxHealth; // Set initial health to max
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        // Check if the enemy is alive
        if (currentHealth <= 0)
        {
            Die();
            return; // Exit the Update method if the enemy is dead
        }

        // Check if the player is in attack range
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // Attack the player
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + timeBetweenAttacks;
                
            }
        }
        else
        {
            // Move towards the player
             Vector2 direction = (player.position - transform.position).normalized;
              rb.velocity = direction * moveSpeed;

            // Set animation parameters 
            animator.SetFloat("Horizontal", direction.x);
           animator.SetFloat("Vertical", direction.y);
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
    */

    
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float chaseRange = 10f;
    public int maxHealth = 100;
    public int currentHealth;

    public Transform player;
    public Animator animator;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isChasing = false;
    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
            return;

        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer < chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasing();
        }
    }

    void FixedUpdate()
    {
        if (isChasing && !isAttacking)
        {
            MoveEnemy();
        }
    }

    void MoveEnemy()
    {
        movement = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("isMoving", true);

        
    }

    void ChasePlayer()
    {
        isChasing = true;
        isAttacking = false;
    }

    void StopChasing()
    {
        isChasing = false;
        isAttacking = false;
        animator.SetBool("isMoving", false);
    }

    void AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        
        // Disable enemy GameObject or any other death-related logic
        Destroy(gameObject, 1f); // Destroy the enemy GameObject after 1 second
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Perform damage to the player
            Debug.Log("Player hit by enemy!");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", false);
        }
    }
    
}