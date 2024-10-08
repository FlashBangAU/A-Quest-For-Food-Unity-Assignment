using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDisplayForLevelAmount : MonoBehaviour
{
    public DataManager dm;
    private int finishedLength;

    public GameObject tutorial;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogWarning("No DataManager instance found in the scene. Ensure DataManager is correctly initialized.");
        }
        else
        {
            dm = DataManager.Instance; // Assign the DataManager instance to dm
        }

        finishedLength = dm.highScoreGetLength();
        switch(finishedLength)
        {
            case 1:
                tutorial.SetActive(true);
                break;
            case 2:
                level1.SetActive(true);
                break;
            case 3:
                level2.SetActive(true);
                break;
            case 4:
                level3.SetActive(true);
                break;
            case 5:
                level4.SetActive(true);
                break;
            case 6:
                level5.SetActive(true);
                break;
        }
    }
}
