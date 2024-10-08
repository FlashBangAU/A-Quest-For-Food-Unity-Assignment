using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerBoss : MonoBehaviour
{
    public GameObject bossRB;
    public GameObject playerGO; // Reference to the player GameObject
    public Rigidbody2D playerRB; // Reference to the player's Rigidbody2D
    private bool canDamage = false; // Flag to indicate if the player can be damaged
    private AudioManager audioManager;

    [SerializeField] private float KBTimer = 0f; // Timer to manage damage timing
    [SerializeField] private float KBInterval = 1.2f; // Time interval between damage applications

    public float knockbackForce = 10f;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        // Update the timer
        KBTimer -= Time.deltaTime;

        // If within the damage interval, apply movement to the player
        if (KBTimer >= 0f && canDamage)
        {
            ApplyKnockback();
        }
    }

    public void ApplyKnockback()
    {
        if (playerRB != null)
        {
            Vector3 knockbackDirection;

            // Resetting velocity may not be necessary; you can comment this out if you want a more natural effect
            playerGO.GetComponent<PlayerMovement>().isKnockedBack = true;
            playerGO.GetComponent<PlayerMovement>().hasBeenAirborne = true;
            playerRB.velocity = Vector2.zero;

            if (playerRB.transform.position.x > bossRB.transform.position.x)
            {
                knockbackDirection = new Vector3(1, 1, 0).normalized;//knock player right
            }
            else
            {
                knockbackDirection = new Vector3(-1, 1, 0).normalized; //knock player left
            }

            playerRB.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            Debug.Log("Knockback Direction: " + knockbackDirection);
            Debug.Log("Knockback Force: " + (knockbackDirection * knockbackForce));
        }
    }


    public void PlayerGotHit()
    {
        if (canDamage && KBTimer <= 0f)
        {
            KBTimer = KBInterval; // Reset the timer
            PlayerHealth playerHealth = playerGO.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.health -= 1; // Reduce player's health
                audioManager.PlaySFX(audioManager.playerDamage);
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
