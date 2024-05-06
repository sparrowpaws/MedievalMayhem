using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //variables for health and health bar
    public int maxHealth = 100; // The maximum health the player can have
    private int PlayerCurrentHealth;  // The current health of the player
    public HealthBar healthBar; // Reference to the health bar script


    private void Start()
    {
        PlayerCurrentHealth = maxHealth; // Set initial health to max
        healthBar.SetSliderMax(maxHealth); // sets the slider to show the max health (100)
    }


// Method to apply damage to the player
public void TakeDamage(int damage)
    {
        
        PlayerCurrentHealth -= damage; // Reduce health by the damage amount
        healthBar.SetSlider(PlayerCurrentHealth); // Update the health bar
        Debug.Log("The player took damage. Now at " + PlayerCurrentHealth + " health.");
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