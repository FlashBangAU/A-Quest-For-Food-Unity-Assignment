using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int health;
    int currentHealth;
    [SerializeField] int maxHealth;
    [SerializeField] int heal = 5;

    [SerializeField] int eatCoinReq = 5;
    [SerializeField] int coinLostDeath = 7;

    public float enemiesDefeated = 0f;

    public CoinScript cs;
    public HealthBar healthBar;

    Vector2 checkPoint;
    [SerializeField] BackGroundMovement bgm;

    AudioManager audioManager;

    void Start()
    {
        checkPoint = transform.position; // Initialize checkpoint to start position
        currentHealth = health; // Initialize current health
        UpdateHealthBar(); // Initialize health bar with current health
    }

    void Update()
    {
        // Healing logic
        if (Input.GetKeyDown(KeyCode.O) && cs.coinCount >= eatCoinReq && health != maxHealth)
        {
            Heal();
        }

        // Update health bar when health changes
        if (currentHealth != health)
        {
            currentHealth = health;
            UpdateHealthBar();
        }

        // Handle player death
        if (health <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Die()
    {
        Respawn();
    }

    void Respawn()
    {
        health = maxHealth; // Reset health to max
        currentHealth = health; // Update currentHealth to maxHealth

        // Deduct coins upon respawn
        if (cs.coinCount >= coinLostDeath)
        {
            cs.coinCount -= coinLostDeath;
        }
        else
        {
            cs.coinCount = 0; // Ensure coin count does not go negative
        }

        transform.position = checkPoint; // Move player to last checkpoint

        // Handle background movement respawn if necessary
        if (bgm != null)
        {
            bgm.Respawn();
        }

        // Update health bar to reflect reset health
        UpdateHealthBar();
    }

    void Heal()
    {
        if (cs.coinCount >= eatCoinReq)
        {
            cs.coinCount -= eatCoinReq; // Deduct coins used for healing
            health = Mathf.Min(health + heal, maxHealth); // Heal player, but cap health at maxHealth
            currentHealth = health; // Sync currentHealth with health
            UpdateHealthBar(); // Update the health bar
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth); // Set the max health in the health bar
            healthBar.UpdateHealth(currentHealth); // Update the health bar with current health
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Detect when player collects a coin
        if (other.gameObject.CompareTag("Coin"))
        {
            audioManager.PlaySFX(audioManager.foodPickup);
            Destroy(other.gameObject);
            cs.coinCount += Random.Range(2, 4); // Add random number of coins to player count
        }

        // Detect when player reaches a checkpoint
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            checkPoint = transform.position; // Update checkpoint position to current player position
            if (bgm != null)
            {
                bgm.NewRespawn(); // Trigger background respawn logic if applicable
            }
        }
    }
}
