using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    EnemyHealth eh;
   


    enum Phase { Phase1, Phase2, Phase3};
    Phase currentPhase;

    // Start is called before the first frame update
    void Start()
    {
        eh = gameObject.GetComponent<EnemyHealth>();
       
        currentPhase = Phase.Phase1;
        Debug.Log(currentPhase);
       
        Debug.Log(currentPhase);
    }

    Phase ChangePhase (Phase P)
    {
        if (P == Phase.Phase1)
            P = Phase.Phase2;
        
        else if (P == Phase.Phase2)
            P = Phase.Phase3;
       




        return P;
    }








    // Update is called once per frame
    void Update()
    {
        if (currentPhase == Phase.Phase1)
        {
            if (eh.health == 8)
            {
                currentPhase = ChangePhase(currentPhase);
                Debug.Log(currentPhase);
            }
        }

        if(currentPhase == Phase.Phase2)
        {
            if (eh.health == 4)
            {
                currentPhase = ChangePhase(currentPhase);
                Debug.Log(currentPhase);
            }
        }

        
    }





}
