using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBossFight : MonoBehaviour
{
    public phaseController pc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && pc.phase0)
        {
            pc.phase0 = false;
            pc.phase1 = true;
        }
    }
}
