using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStomp : MonoBehaviour
{
    public float bounce;
    public Rigidbody2D rb2D;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weak Point"))
        {
            Destroy(other.gameObject);
            rb2D.velocity = new Vector2(rb2D.velocity.x, bounce);
        }
    }
}