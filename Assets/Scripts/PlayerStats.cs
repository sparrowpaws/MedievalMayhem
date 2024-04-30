using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //variables for health and health bar
    public int maxHealth = 100; // The maximum health the player can have
    private int PlayerCurrentHealth;  // The current health of the player
    public HealthBar healthBar; // Reference to the health bar script

    //variables for attack
    public int attackDamage = 30;
    public float attackCooldown = 2f; // Time between attacks (1 second)
    private float lastAttackTime = 0f; // Time of the last attack
    public float attackRange = 2f;
    public LayerMask allLayers;  //may need to change this and put it on the same layer as the enemy collider
    private Vector2 attackDirection; // Variable to track attack direction

    private void Start()
    {
        PlayerCurrentHealth = maxHealth; // Set initial health to max
        healthBar.SetSliderMax(maxHealth); // sets the slider to show the max health (100)
    }

    private void Update()
    {
        // Determine the player's direction based on movement input (example using arrow keys)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            attackDirection = Vector2.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            attackDirection = Vector2.down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            attackDirection = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            attackDirection = Vector2.right;
        }

        // Check if the space bar is pressed and cooldown has elapsed
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastAttackTime + attackCooldown)
        {
            Debug.Log("player is attacking");
            Attack();
            lastAttackTime = Time.time; //update the last attack time
        }
    }

    private void Attack()
    {
        Debug.Log("Attack called");

        // Perform the raycast to detect if it hits an enemy
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, attackRange, allLayers);

        if (hit.collider != null) // Check if the raycast hits anything
        {
            Debug.Log("Raycast hit something");

            if (hit.collider.CompareTag("Enemy")) // Check if it has the correct tag
            {
                Debug.Log("Raycast hit an enemy");
                Enemy targetEnemy = hit.collider.GetComponent<Enemy>(); // Get Enemy component
                if (targetEnemy != null)
                {
                    targetEnemy.TakeDamage(attackDamage); // Apply damage to the enemy
                    lastAttackTime = Time.time; // Reset cooldown
                }
            }
        }
        else
        {
            Debug.Log("Raycast didn't hit anything");
        }
    }

// Method to apply damage to the player
public void TakeDamage(int damage)
    {
        PlayerCurrentHealth -= damage; // Reduce health by the damage amount
        healthBar.SetSlider(PlayerCurrentHealth); // Update the health bar
        if (PlayerCurrentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        PlayerCurrentHealth += amount;
        //this if statement keeps from healing over max health
        if(PlayerCurrentHealth > maxHealth)
        {
            PlayerCurrentHealth = maxHealth;
        }
        healthBar.SetSlider(PlayerCurrentHealth);
    }

    // Method to handle player death
    private void Die()
    {
        // Handle player death logic here (e.g., play animation, trigger game over, etc.)
        Debug.Log("Player has died");

    }
}