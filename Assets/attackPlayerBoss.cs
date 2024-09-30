using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerBoss : MonoBehaviour
{
    public Phase1Crow p1c; // Reference to the boss (not currently used, but can be useful for future features)
    public GameObject playerGO; // Reference to the player GameObject
    public Rigidbody2D playerRB; // Reference to the player's Rigidbody2D for applying knockback
    private bool canDamage = false; // Flag to indicate if the player can be damaged

    [SerializeField] private float KBTimer = 0f; // Timer to manage the timing of damage application
    [SerializeField] private float KBInterval = 1.2f; // Time interval between damage applications to the player

    public float knockbackForce = 10f; // Force applied to the player when they are hit

    private void FixedUpdate()
    {
        // Update the knockback timer by reducing it with the elapsed time
        KBTimer -= Time.deltaTime;

        // If the timer is still valid (greater than or equal to 0), apply knockback to the player
        if (KBTimer >= 0f)
        {
            // Reset player's velocity to zero before applying knockback
            playerRB.velocity = Vector2.zero;
            Vector2 knockbackDirection = new Vector2(-1, 1).normalized; // Calculate knockback direction (left-up)

            // Apply force to the player's Rigidbody2D in the knockback direction
            playerRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    // Method called when the player is hit by the boss
    public void PlayerGotHit()
    {
        // Check if the player can currently be damaged
        if (canDamage)
        {
            // Reset the knockback timer to the specified interval
            KBTimer = KBInterval;

            // Access the PlayerHealth component to reduce the player's health
            PlayerHealth playerHealth = playerGO.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health -= 1; // Decrease player's health by 1
            }
            else
            {
                // Log a warning if the PlayerHealth component is not found
                Debug.LogWarning("PlayerHealth component not found on playerGO!");
            }
        }
    }

    // Trigger event when another collider enters the trigger collider attached to this GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            canDamage = true; // Allow damage to the player
             Debug.Log("Player can be damaged"); // Uncomment for debugging purposes
        }
    }

    // Trigger event when another collider exits the trigger collider attached to this GameObject
    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            canDamage = false; // Prevent damage to the player
             Debug.Log("Player can't be damaged"); 
        }
    }
}
