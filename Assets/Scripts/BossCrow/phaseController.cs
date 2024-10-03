using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class phaseController : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] public int health;
=======
    [SerializeField] int health;
>>>>>>> sound-branch
    [SerializeField] public bool phase0;
    [SerializeField] public bool phase1;
    [SerializeField] public bool phase2;
    [SerializeField] public bool phase3;
    bool isDefeated = false;

<<<<<<< HEAD
    void Start()
    {
        //phase3 = true; // Automatically start in Phase 3 for testing
    }


=======
>>>>>>> sound-branch
    // Update is called once per frame
    void Update()
    {
        if (phase1)
        {

        }else if (phase2)
        {

        }else if (phase3)
        {

        }else if (isDefeated)
        {

        }
    }

    public void removehealth()
    {
        health --;
        if(health <= 0)
        {
            phase3 = false;
            isDefeated = true;
        }
        else if (health <= 4)
        {
            phase2 = false;
            phase3 = true;
        }
        else if (health <= 8)
        {
            phase1 = false;
            phase2 = true;
        }
    }
}
