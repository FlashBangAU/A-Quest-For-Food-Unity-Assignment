using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLeft : MonoBehaviour
{
    public bool canJump = true;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
            Debug.Log("Collision with ground left");
        }
    }

    // Prevents player from walking off platform and jumping
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = false;
            Debug.Log("Left ground");
        }
    }
}
