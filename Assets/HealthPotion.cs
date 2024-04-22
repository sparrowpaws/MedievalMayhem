using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : MonoBehaviour
{
    public Button potionButton; // The UI button for the potion
    public float cooldownTime = 10f; // The cooldown time in seconds
    private float currentCooldown; // Tracks the current cooldown time
    public int healingAmount = 30; // The amount of health restored by the potion
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script

    void Start()
    {
        currentCooldown = 0f; // Start with no cooldown
        potionButton.interactable = true; // The potion is initially available
        potionButton.onClick.AddListener(UsePotion); // Add click listener to the button
    }

    void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime; // Decrease cooldown over time

            if (currentCooldown <= 0)
            {
                potionButton.interactable = true; // Enable the button when cooldown is over
            }
        }
    }

    // Method to use the potion and reset cooldown
    void UsePotion()
    {
        if (currentCooldown <= 0)
        {
            if (playerHealth != null)
            {
                playerHealth.Heal(healingAmount); // Heal the player
            }

            currentCooldown = cooldownTime; // Reset the cooldown timer
            potionButton.interactable = false; // Disable the button until cooldown is over
        }
    }
}

