using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBossFight : MonoBehaviour
{
    public phaseController pc; // Reference to the phaseController script to manage boss phases

    // This method is called when another collider enters the trigger collider attached to this GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object that entered the trigger has the tag "Player"
        // and that the boss is currently in phase 0
        if (collision.CompareTag("Player") && pc.phase0)
        {
            // Change the boss phase from phase 0 to phase 1 when the player triggers the boss fight
            pc.phase0 = false; // Set phase 0 to false, indicating it is no longer active
            pc.phase1 = true; // Activate phase 1 of the boss fight
        }
    }
}
