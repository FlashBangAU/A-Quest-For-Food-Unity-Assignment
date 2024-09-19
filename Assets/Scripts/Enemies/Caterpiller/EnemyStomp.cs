using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb2D;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weak Point"))
        {
            Destroy(other.gameObject);
            audioManager.PlaySFX(audioManager.enemySquish);
            rb2D.velocity = new Vector2(rb2D.velocity.x, bounce);
        }
    }
}