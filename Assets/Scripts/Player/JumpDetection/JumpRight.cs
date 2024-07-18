using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRight : MonoBehaviour
{
    public bool canJump = true;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        Debug.Log("Collision with ground");
    }

    // Prevents player from walking off platform and jumping
    public void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
        Debug.Log("Left ground");
    }
}
