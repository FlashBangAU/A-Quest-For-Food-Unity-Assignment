using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfHit : MonoBehaviour
{
    public phaseController pc; // Reference to the phase controller for managing boss phases
    public Phase1Crow p1c; // Reference to the Phase1Crow script for managing the crow boss

    // Method called when the boss gets hit
    public void bossGotHit()
    {
        Debug.Log("Boss has been damaged"); // Log to the console that the boss has been hit
        pc.removehealth(); // Call the method to reduce the boss's health and potentially change phases
        p1c.unStuck(); // Call the method to make the boss vulnerable again, allowing it to move or attack
    }
}
