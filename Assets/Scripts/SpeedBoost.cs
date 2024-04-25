/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public int boostAmount = 5;
    public float cooldownTime = 7f; // Cooldown time in seconds
    private float lastUseTime; // Time when the potion was last used
    private PlayerStats playerStats; // Reference to PlayerStats instance

    private void Start()
    {
        // Initialize the last use time to allow immediate first use
        lastUseTime = -cooldownTime;

        // Find the PlayerStats instance in the scene
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats instance not found in the scene!");
        }
    }
    private void Update()
    {
        if (playerStats != null && Time.time - lastUseTime >= cooldownTime)
        {
            playerStats.Boost(boostAmount);
            lastUseTime = Time.time; // Update the last use time
            Debug.Log("speed boost used. Cooldown initiated.");
        }
        else
        {
            Debug.Log("Cannot use Speed boost. Cooldown not yet over.");
        }
    }
} */
using UnityEngine;
using System.Collections;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 5f; // The amount by which speed is increased
    public float boostDuration = 5f; // The duration for which the boost is active
    public float cooldownDuration = 10f; // The cooldown time between speed boosts

    private CharacterMovement characterMovement; // Reference to the movement script
    private bool isBoosting = false; // Flag to check if boost is active
    private bool isOnCooldown = false; // Flag to check if cooldown is active

    private void Start()
    {
        // Get reference to the CharacterMovement script
        characterMovement = FindObjectOfType<CharacterMovement>();
        if (characterMovement == null)
        {
            Debug.LogError("CharacterMovement script not found on any GameObject.");
        }
    }

    private void Update()
    {
        // Activate speed boost when 'E' key is pressed if it's not on cooldown
        if (Input.GetKeyDown(KeyCode.E) && !isOnCooldown)
        {
            StartCoroutine(SpeedBoostCoroutine());
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        isOnCooldown = true; // Start cooldown
        isBoosting = true; // Speed boost is active

        float originalSpeed = characterMovement.moveSpeed; // Store the original speed
        characterMovement.moveSpeed += boostAmount; // Increase speed

        Debug.Log("Speed boost activated!");

        yield return new WaitForSeconds(boostDuration); // Wait for the boost duration

        characterMovement.moveSpeed = originalSpeed; // Reset to the original speed
        isBoosting = false; // Speed boost ended

        Debug.Log("Speed boost ended. Player speed reset.");

        yield return new WaitForSeconds(cooldownDuration - boostDuration); // Wait for the cooldown

        isOnCooldown = false; // Cooldown complete
        Debug.Log("Cooldown ended. Speed boost ready to use again.");
    }
}
