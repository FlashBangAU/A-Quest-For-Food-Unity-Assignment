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

    public float maxTime;
    [SerializeField]  float timer = 0f;
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
        timer = Time.time;
        showScoreTitleUI.text = "";
        showScoreUI.text = "";

    }

    private void Update()
    {
        if (!hasFinished)
        {
            timer = Time.time;
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
            timer = maxTime - timer;

            if (timer < 0f)
                timer = 0f;

            //all variables are 0
            if (timer == 0f && cs.coinCount == 0f && ph.enemiesDefeated == 0f)
            {
                score = 0f;
            }


            //only time left
            else if (cs.coinCount == 0f && ph.enemiesDefeated == 0f)
            {
                score = timer * timerMultiplier;
            }
            //only enemies killed
            else if (timer == 0f && cs.coinCount == 0f)
            {
                score = ph.enemiesDefeated * enemiesMultiplier;
            }
            //only coins collected
            else if (timer == 0f && ph.enemiesDefeated == 0f)
            {
                score = cs.coinCount * coinMultiplier;
            }


            //no enemies killed
            else if (ph.enemiesDefeated == 0f)
            {
                score = (timer * timerMultiplier) * (cs.coinCount * coinMultiplier);
            }
            //no coins collected
            else if (cs.coinCount == 0f)
            {
                score = (timer * timerMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
            }
            //no time left for point (took too long)
            else if (timer == 0f)
            {
                score = (cs.coinCount * coinMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
            }
            
            //all variables filled
            else
            {
                score = (timer * timerMultiplier) * (cs.coinCount * coinMultiplier) * (ph.enemiesDefeated * enemiesMultiplier);
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