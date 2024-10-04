using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;

    private GameObject player;
    public HealthBar healthBar;
    private PlayerHealth ph;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ph = player.GetComponent<PlayerHealth>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.UpdateHealth(health);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            ph.enemiesDefeated += 1f;
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioManager.PlaySFX(audioManager.enemyHurt);
        healthBar.UpdateHealth(health);
        Debug.Log("Enemy Takes Damage");
    }
}
