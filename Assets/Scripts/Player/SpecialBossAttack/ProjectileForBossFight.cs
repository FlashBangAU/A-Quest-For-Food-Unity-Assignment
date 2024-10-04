using UnityEngine;

public class ProjectileForBossFight : MonoBehaviour
{
    private GameObject boss;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float xLimit;
    [SerializeField] private float yLimit;

    private Vector3 bossPos;
    private Vector2 direction;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("BossSpeicalHitBox");

        // Initialize direction towards the boss
        direction = ((boss.transform.position) - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    void Update()
    {
        // Update direction towards the boss
        bossPos = new Vector3(boss.transform.position.x - 3, boss.transform.position.y);
        Vector2 newDirection = (bossPos - transform.position).normalized;

        // Limit the movement based on last direction
        newDirection.x = Mathf.Clamp(newDirection.x, direction.x - xLimit, direction.x + xLimit);
        newDirection.y = Mathf.Clamp(newDirection.y, direction.y - yLimit, direction.y + yLimit);

        // Update velocity and rotation
        direction = newDirection;

        rb.velocity = direction * speed;

        timer += Time.deltaTime;

        if (timer > 8)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossSpeicalHitBox"))
        {
            collision.gameObject.GetComponent<phaseController>().removehealth();
            Destroy(gameObject);
        }
    }
}
