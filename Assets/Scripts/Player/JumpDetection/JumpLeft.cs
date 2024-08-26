using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLeft : MonoBehaviour
{
    public bool canJump = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = true;
            //Debug.Log("Collision with ground left");
        }
    }

    // Prevents player from walking off platform and jumping
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            canJump = false;
            //Debug.Log("Left ground");
        }
    }
}
