using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase3Crow : MonoBehaviour
{
    public phaseController pc;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pc.phase3 == true)
        {
            //Write movement here, I have added created game objects in "test functions" scene called "Branch1" and "Branch2".
            //Bird is to fly up branch high (both have same y cordinates) and 'perch' somewhere between these 2 points at random on x.
            //The bird will swoopdown to players positon at beginning of swoop allowing player to dodge swoop attack.
            //After bird reaches player recorded position bird goes back up to perach for another swoop.

            //currently only worry about bird movement not attack or damage.
        }
    }
}
