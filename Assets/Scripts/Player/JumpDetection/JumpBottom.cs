using UnityEngine;

public class JumpBottom : MonoBehaviour
{
    public bool canJump = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we collided with has the "Ground" tag
        if (other.CompareTag("Ground"))
        {
            canJump = true;
            // Optionally, log to the console for debugging
            //Debug.Log("Collision with ground bottom");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the object we exited has the "Ground" tag
        if (other.CompareTag("Ground"))
        {
            canJump = false;
            // Optionally, log to the console for debugging
            //Debug.Log("Left ground");
        }
    }
}
