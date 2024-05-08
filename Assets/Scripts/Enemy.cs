using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 80; 
    public int currentHealth; 

    public float moveSpeed = 3f;
    public float attackRange = 1.5f; // range for attacking player
    public float triggerRange = 5f; // range for chasing player

    public float timeBetweenAttacks = 2f;
    public int attackDamage = 10; 
    
    public float attackCooldown = 1.5f;
    private float lastAttackTime = 0f;
    private float nextAttackTime;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
   
    

    void Start()
    {
        currentHealth = maxHealth; // Set initial health to max
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

    void Update()
    {
        // check if the enemy is alive, then move.
        if (currentHealth <= 0)
        {
            Die();
            return; // exit if the enemy is dead
        }
        
        Move();
        
    }


    void Move()
    {   
        //only move if player is nearby
        if (Vector2.Distance(transform.position, player.position) <= triggerRange)
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
        } else
        {
            animator.SetBool("isMoving", false);
        }
    }

    void Attack()
    {
       
        Debug.Log("Enemy attacks!");

        animator.SetBool("isAttacking", true);

        // Damage the player
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(attackDamage);
        }

    }

    // enemy takes damage
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

    // enemy dies
    void Die()
    {
        //add death animation here???
        Debug.Log("Enemy defeated!");
        Destroy(gameObject); // Destroy the enemy GameObject
    }

}