using UnityEngine;

public class StemBorerAttack : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private Transform projPos;

    private float timer;
    private GameObject player;

    private float timeNectShot;

    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeNectShot = Random.Range(3, 6);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < 8)
        {
            timer += Time.deltaTime;

            if (timer > timeNectShot)
            {
                timer = 0;
                shoot();
                audioManager.PlaySFX(audioManager.enemyAttack);
                timeNectShot = Random.Range(3, 6);
            }
        }
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //shoot projectile
    void shoot()
    {
        if (projectile != null)
        {
            Instantiate(projectile, projPos.position, Quaternion.identity);
        }
    }
}
