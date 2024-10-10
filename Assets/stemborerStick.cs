using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stemborerStick : MonoBehaviour
{
    Rigidbody2D rb;

    public bool isHeld = false;
    public GameObject Beak;

    public Transform spawnPos;
    public GameObject stemBorerBoss;

    private float spawnNewEnemy;
    [SerializeField] float spawnInterval;

    public bool onGround;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeld)
        {
            Vector2 beakPos = Beak.transform.position;
            rb.transform.position = beakPos;
            rb.bodyType = RigidbodyType2D.Static;

            spawnNewEnemy += Time.deltaTime;

            if (spawnNewEnemy > spawnInterval)
            {
                Instantiate(stemBorerBoss, spawnPos.position, Quaternion.identity);
                audioManager.PlaySFX(audioManager.foodPickup);
                spawnNewEnemy = 0f;
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            spawnNewEnemy = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}