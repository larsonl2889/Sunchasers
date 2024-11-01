using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject player;

    
    public void changeMenuState()
    {
        if (GameIsPaused == false)
        {
            pause();
        }
        else
        {
            resume();
        }
    }
    public void resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
    public void pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
        Debug.Log("Pause");
    }
    public void quitGame()
    {
        SceneManager.LoadScene("Title");
    }
}
