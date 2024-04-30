using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
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
        }
    }

    void Attack()
    {
        // Perform attack action here
        Debug.Log("Enemy attacks!");

        //need to fix so that isAttacking stays true/false the right amount of timeee
        animator.SetBool("isAttacking", true);

        // Damage the player
        PlayerStats playerStats = player.GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.TakeDamage(attackDamage);
        }
        
        animator.SetBool("isAttacking", false);
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
        // Perform death actions here (e.g., play death animation, disable GameObject, etc.)
        Debug.Log("Enemy defeated!");
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}