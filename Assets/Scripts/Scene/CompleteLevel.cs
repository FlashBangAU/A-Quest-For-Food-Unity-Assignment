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

    public float maxTime;
    [SerializeField]  float theTimeInSec;
    float score;

    [SerializeField] public int showScore = 0;

    [SerializeField] float timerMultiplier;
    [SerializeField] float coinMultiplier;
    [SerializeField] float enemiesMultiplier;

    [SerializeField] float timerEndScene = 0f;
    [SerializeField] float timeEndScene = 5f;

    public SceneController sceneController;

    private void Start()
    {
        showScoreTitleUI.text = "";
        showScoreUI.text = "";

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

            //all variables are 0
            if (theTimeInSec == 0f && cs.coinCount == 0f && ph.enemiesDefeated == 0f)
            {
                score = 0f;
            }


            //only time left
            else if (cs.coinCount == 0f && ph.enemiesDefeated == 0f)
            {
                score = theTimeInSec * timerMultiplier;
            }
            //only enemies killed
            else if (theTimeInSec == 0f && cs.coinCount == 0f)
            {
                score = ph.enemiesDefeated * enemiesMultiplier;
            }
            //only coins collected
            else if (theTimeInSec == 0f && ph.enemiesDefeated == 0f)
            {
                score = cs.coinCount * coinMultiplier;
            }


            //no enemies killed
            else if (ph.enemiesDefeated == 0f)
            {
                score = (theTimeInSec * timerMultiplier) * (cs.coinCount * coinMultiplier);
            }
            //no coins collected
            else if (cs.coinCount == 0f)
            {
                score = (theTimeInSec * timerMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
            }
            //no time left for point (took too long)
            else if (theTimeInSec == 0f)
            {
                score = (cs.coinCount * coinMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
            }
            
            //all variables filled
            else
            {
                score = (theTimeInSec * timerMultiplier) * (cs.coinCount * coinMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
            }

            score *= 1000;
            showScore = (int)score;
            hasFinished = true;
            DisplayScoreUI();
        }
    }

    public void DisplayScoreUI()
    {
        showScoreTitleUI.text = "SCORE";
        showScoreUI.text = showScore.ToString("N0");
    }
}