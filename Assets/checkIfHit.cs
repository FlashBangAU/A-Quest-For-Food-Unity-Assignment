using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkIfHit : MonoBehaviour
{
    public phaseController pc;
    public Phase1Crow p1c;
    public Phase2Crow p2c;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void bossGotHit()
    {
        Debug.Log("Boss has been damaged");
        pc.removehealth();
        audioManager.PlaySFX(audioManager.bossHurt);
        if (pc.phase1 == true)
        {
            p1c.unStuck();
        } else if (pc.phase2 == true)
        {
            p2c.PickUpStick();
        }
    }
}
