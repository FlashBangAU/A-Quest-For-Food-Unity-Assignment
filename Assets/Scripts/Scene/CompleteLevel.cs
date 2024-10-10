using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    bool hasFinished = false;

    public PlayerHealth ph;
    public CoinScript cs;
    public TextMeshProUGUI showScoreTitleUI;
    public TextMeshProUGUI showScoreUI;
    public TextMeshProUGUI TimeUI;
    public TextMeshProUGUI showHighScoreTitleUI;
    public TextMeshProUGUI showHighScoreUI;

    public DataManager dm;
    public int level;
    float[] highScores;

    public float maxTime;
    [SerializeField] float theTimeInSec;
    float score;

    [SerializeField] public int showScore = 0;

    [SerializeField] float timerMultiplier;
    [SerializeField] float coinMultiplier;
    [SerializeField] float enemiesMultiplier;

    [SerializeField] float timerEndScene = 0f;
    [SerializeField] float timeEndScene = 5f;

    public SceneController sceneController;

    float curHighScore;

    public bool talkingNPC = false;

    public bool removeAllSaveData = false;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
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

        showScoreTitleUI.text = "";
        showScoreUI.text = "";
        showHighScoreTitleUI.text = "";
        showHighScoreUI.text = "";
    }

    private void Update()
    {
        // Update timer if the game is not finished
        if (!hasFinished)
        {
            // If talking to an NPC, do not update the timer
            if (!talkingNPC)
            {
                theTimeInSec += Time.deltaTime;  // Use deltaTime for smooth updates
                UpdateTimeUI();
            }
        }
        else
        {
            // Handle end scene timer
            timerEndScene += Time.deltaTime;
            if (timerEndScene >= timeEndScene)
            {
                sceneController.startMenu();
            }
        }
    }

    private void UpdateTimeUI()
    {
        int HH = Mathf.FloorToInt(theTimeInSec / 3600.0f);
        theTimeInSec %= 3600.0f;
        int MM = Mathf.FloorToInt(theTimeInSec / 60.0f);
        theTimeInSec %= 60.0f;
        int SS = Mathf.FloorToInt(theTimeInSec);
        int MILLISECONDS = Mathf.FloorToInt((theTimeInSec - SS) * 1000.0f);

        // Update the UI text with formatted time
        TimeUI.text = $"{MM:00}:{SS:00}:{MILLISECONDS:000}";
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            theTimeInSec = maxTime - theTimeInSec;

            if (theTimeInSec < 0f)
                theTimeInSec = 0f;

            // Calculate score based on game state
            score = CalculateScore();

            score *= 1000;
            showScore = (int)score;
            hasFinished = true;

            audioManager.PlaySFX(audioManager.levelComplete);

            CheckHighScore();

            DisplayScoreUI();
        }
    }

    float CalculateScore()
    {
        if (theTimeInSec == 0f && cs.coinCount == 0f && ph.enemiesDefeated == 0f)
        {
            return 0f;
        }
        else if (cs.coinCount == 0f && ph.enemiesDefeated == 0f)
        {
            return theTimeInSec * timerMultiplier;
        }
        else if (theTimeInSec == 0f && cs.coinCount == 0f)
        {
            return ph.enemiesDefeated * enemiesMultiplier;
        }
        else if (theTimeInSec == 0f && ph.enemiesDefeated == 0f)
        {
            return cs.coinCount * coinMultiplier;
        }
        else if (ph.enemiesDefeated == 0f)
        {
            return (theTimeInSec * timerMultiplier) * (cs.coinCount * coinMultiplier);
        }
        else if (cs.coinCount == 0f)
        {
            return (theTimeInSec * timerMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
        }
        else if (theTimeInSec == 0f)
        {
            return (cs.coinCount * coinMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
        }
        else
        {
            return (theTimeInSec * timerMultiplier) * (cs.coinCount * coinMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
        }
    }

    public void DisplayScoreUI()
    {
        showScoreTitleUI.text = "SCORE";
        showScoreUI.text = showScore.ToString("N0");
    }

    public void CheckHighScore()
    {
        if (dm == null) return; // Check if DataManager is not initialized

        curHighScore = dm.GetHighScoreForLevel(level);
        if (curHighScore <= showScore)
        {
            DisplayNewHighScore();
            // Save new highscore
            dm.highScore = showScore;
            dm.level = level;
            dm.WriteData();
        }
        else
        {
            DisplayCurrentHighScore();
        }
        if (removeAllSaveData)
        {
            dm.highScore = 0f;
            dm.level = level;
            dm.highScores = highScores;
            dm.WriteData();
        }
    }

    public void DisplayNewHighScore()
    {
        showHighScoreUI.text = "NEW HIGHSCORE!!!";
    }

    public void DisplayCurrentHighScore()
    {
        showHighScoreUI.text = curHighScore.ToString("N0");
        showHighScoreTitleUI.text = "HIGHSCORE";
    }
}
