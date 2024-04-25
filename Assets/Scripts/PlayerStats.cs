using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100; // The maximum health the player can have
    private int currentHealth;  // The current health of the player

    public HealthBar healthBar; // Reference to the health bar script

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth; // Set initial health to max

        healthBar.SetSliderMax(maxHealth); //sets the slider to show the max health (100)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
       // if (Input.GetKeyDown(KeyCode.Q))
      //  {
       //     Heal(30);
     //   }
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by the damage amount
        healthBar.SetSlider(currentHealth); // Update the health bar
        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    // Method to heal the player
    public void Heal(int amount)
    {
        currentHealth += amount;
        //this if statement keeps from healing over max health
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetSlider(currentHealth);
    }

    // Method to handle player death
    private void Die()
    {
        // Handle player death logic here (e.g., play animation, trigger game over, etc.)
        Debug.Log("Player has died");

    }
}