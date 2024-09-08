using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUnpauseGame : MonoBehaviour
{
    private GameObject pausescreen;

    // Start is called before the first frame update
    void Start()
    {
        pausescreen = GameObject.FindGameObjectWithTag("Pausedscreen");
        if (pausescreen == null)
        {
            Debug.Log("Error pause not found");
        }
        else
        {
            //ensure that loading new scene is not paused
            Time.timeScale = 1f;
            pausescreen.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseunpause();
        }
    }

    //pause
    public void pauseunpause()
    {       //unpause
        if (pausescreen.activeInHierarchy)
        {
            pausescreen.SetActive(false);
            Time.timeScale = 1f;

            // if needed to lock the cursor
            //Cursor.lockState = CursorLockMode.Locked;

        }
        else //puase
        {
            pausescreen.SetActive(true);
            Time.timeScale = 0f;

            //if needed to umlock the cursor
            //Cursor.lockState = CursorLockMode.None;

        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
