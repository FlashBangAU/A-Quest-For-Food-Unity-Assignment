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

    // Start is called before the first frame update
    void Start()
    {
        checkPoint = transform.position;
        if (healthBar != null) {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.UpdateHealth(health);
        }
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        //heal
        if (Input.GetKeyDown(KeyCode.O) && cs.coinCount >= eatCoinReq && health != maxHealth)
        {
            Heal();
        }

        //player is damaged
        if (currentHealth != health)
        {
            healthBar.UpdateHealth(health);
            currentHealth = health;
        }

        //player dies
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //add death animation
        Respawn();
    }

    void Respawn()
    {
        health = maxHealth;
        if (cs.coinCount >= coinLostDeath)
        {
            cs.coinCount = cs.coinCount - coinLostDeath;
        } else
        {
            cs.coinCount = 0;
        }
        transform.position = checkPoint;
        if (bgm != null)
        {
            bgm.Respawn();
        }
        
    }

    void Heal()
    {
        cs.coinCount -= eatCoinReq;
        if ((heal + health) >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += heal;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cs.coinCount += Random.Range(2, 4);
        }

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            checkPoint = transform.position;
            if (bgm != null)
            {
                bgm.NewRespawn();
            }
        }
    }
}
