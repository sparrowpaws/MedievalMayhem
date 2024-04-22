using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // The maximum health the player can have
    public int currentHealth;  // The current health of the player
    public HealthBar healthBar; // Reference to the health bar script

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; // Set initial health to max
        if (healthBar != null)
        {
            healthBar.SetSliderMax(maxHealth); // Set max health in the health bar
        }
    }

    // Method to apply damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by the damage amount
        if (healthBar != null)
        {
            healthBar.SetSlider(currentHealth); // Update the health bar
        }

        if (currentHealth <= 0)
        {
            Die(); // Handle player death
        }
    }

    // Method to heal the player
    public void Heal(int healingAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healingAmount, maxHealth); // Heal but not above max
        if (healthBar != null)
        {
            healthBar.SetSlider(currentHealth); // Update the health bar
        }
    }

    // Method to handle player death
    private void Die()
    {
        // Handle player death logic here (e.g., play animation, trigger game over, etc.)
        Debug.Log("Player has died");

