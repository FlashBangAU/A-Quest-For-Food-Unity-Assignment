using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfHit : MonoBehaviour
{
    public phaseController pc;
    public Phase1Crow p1c;
    public void bossGotHit()
    {
        Debug.Log("Boss has been damaged");
        pc.removehealth();
        p1c.unStuck();
    }
}
