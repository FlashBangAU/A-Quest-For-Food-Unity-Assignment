using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectLevel : MonoBehaviour
{
    public int level;
    float highScore;
    public DataManager dm;
    public TextMeshProUGUI instructionalQueueUI;
    public TextMeshProUGUI highScoreUI;
    public SceneController sc;

    bool canSelect = false;
    bool canPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        // Check for DataManager instance
        if (DataManager.Instance == null)
        {
            Debug.LogWarning("No DataManager instance found in the scene. Ensure DataManager is correctly initialized.");
        }
        else
        {
            dm = DataManager.Instance; // Assign the DataManager instance to dm
        }

        if (dm != null) // Ensure dm is not null before using it
        {
            highScore = dm.GetHighScoreForLevel(level);
            highScoreUI.text = highScore.ToString("N0");
        }

        instructionalQueueUI.text = "";
        if (level == 0)
        {
            canPlay = true;
        }
        else if (dm.GetHighScoreForLevel(level - 1) > 0)
        {
            canPlay = true;
        }
        else
        {
            canPlay = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canSelect && canPlay)
        {
            sc.startLevel(level);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canPlay)
            instructionalQueueUI.text = "[E]";
        else
            instructionalQueueUI.text = "Level is locked.";
        canSelect = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        instructionalQueueUI.text = "";
        canSelect = false;
    }
}
