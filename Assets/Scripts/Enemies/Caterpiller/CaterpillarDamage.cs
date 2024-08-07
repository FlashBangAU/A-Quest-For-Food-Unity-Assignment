using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarDamage : MonoBehaviour
{
    public float knockbackForce = 10f;
    private bool canDamage = true;
    public float damageCooldown = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canDamage)
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health -= 1;
                KnockbackPlayer(other);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private void KnockbackPlayer(Collider2D player)
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
            playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canDamage = true;
    }
}