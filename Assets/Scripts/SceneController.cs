using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void startMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void startTutorial()
    {
        SceneManager.LoadScene(1);
    }
    public void startLvl1()
    {
        SceneManager.LoadScene(2);
    }
}
