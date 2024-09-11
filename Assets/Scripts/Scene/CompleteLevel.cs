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
        if (!hasFinished)
        {
            theTimeInSec = Time.timeSinceLevelLoad;
            int HH = Mathf.FloorToInt(theTimeInSec / 3600.0f);
            theTimeInSec %= 3600.0f;
            int MM = Mathf.FloorToInt(theTimeInSec / 60.0f);
            theTimeInSec %= 60.0f;
            int SS = Mathf.FloorToInt(theTimeInSec / 1.0f);
            theTimeInSec %= 1.0f;
            int MILLISECONDS = Mathf.FloorToInt(theTimeInSec * 1000.0f);
            //Debug.Log(MM.ToString("00") + ":" + SS.ToString("00") + ":" + MILLISECONDS.ToString("000"));
            TimeUI.text = MM.ToString("00") + ":" + SS.ToString("00") + ":" + MILLISECONDS.ToString("000");
        }
        else if (hasFinished)
        {
            timerEndScene += Time.deltaTime;
            if (timerEndScene >= timeEndScene)
            {
                sceneController.startMenu();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasFinished)
        {
            theTimeInSec = maxTime - theTimeInSec;

            if (theTimeInSec < 0f)
                theTimeInSec = 0f;

            // Calculate score based on game state
            score = (theTimeInSec * timerMultiplier) + (cs.coinCount * coinMultiplier) + (ph.enemiesDefeated * enemiesMultiplier);

            score *= 1000;
            showScore = (int)score;
            hasFinished = true;

            audioManager.PlaySFX(audioManager.levelComplete);

            CheckHighScore();

            DisplayScoreUI();
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
