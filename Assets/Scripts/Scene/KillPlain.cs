using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlain : MonoBehaviour
{
    public PlayerHealth playerHealth;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.health = 0;
        }
    }
}
