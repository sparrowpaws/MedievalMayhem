using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : MonoBehaviour
{
    public int healingAmount = 50; // Amount of health to heal
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
        if (Input.GetKeyDown(KeyCode.Q) && Time.time - lastUseTime >= cooldownTime)
        {
            UsePotion();
        }
    }

    public void UsePotion()
    {
        if (playerStats != null && Time.time - lastUseTime >= cooldownTime)
        {
            playerStats.Heal(healingAmount);
            lastUseTime = Time.time; // Update the last use time
            Debug.Log("Potion used. Cooldown initiated.");
        }
        else
        {
            Debug.Log("Cannot use potion. Cooldown not yet over.");
        }
    }
}







