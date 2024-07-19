using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRight : MonoBehaviour
{
    public bool canJump = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            Debug.Log("Collision with ground right");
        }
    }

    // Prevents player from walking off platform and jumping
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
            Debug.Log("Left ground");
        }
    }
}
