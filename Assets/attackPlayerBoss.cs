using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AttackPlayerBoss : MonoBehaviour
{
    public Phase1Crow p1c; // Reference to the boss (not currently used)
    public GameObject playerGO; // Reference to the player GameObject
    public Rigidbody2D playerRB; // Reference to the player's Rigidbody2D
    private bool canDamage = false; // Flag to indicate if the player can be damaged

    [SerializeField] private float KBTimer = 0f; // Timer to manage damage timing
    [SerializeField] private float KBInterval = 1.2f; // Time interval between damage applications

    public float knockbackForce = 10f;

    private void FixedUpdate()
    {
        // Update the timer
        KBTimer -= Time.deltaTime;

        // If within the damage interval, apply movement to the player
        if (KBTimer >= 0f)
        {
            playerRB.velocity = Vector2.zero;
            Vector2 knockbackDirection = new Vector2(-1, 1).normalized; // left-up direction
            playerRB.velocity = Vector2.zero;
            playerRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    public void PlayerGotHit()
    {
        if (canDamage)
        {
            //Debug.Log("Player Got Hit");
            KBTimer = KBInterval; // Reset the timer
            PlayerHealth playerHealth = playerGO.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health -= 1; // Reduce player's health
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on playerGO!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDamage = true; // Allow damage to the player
            //Debug.Log("Player can be damaged");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDamage = false; // Prevent damage to the player
            //Debug.Log("Player can't be damaged");
        }
    }
}
