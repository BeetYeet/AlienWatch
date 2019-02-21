using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Ondeath;
    public GameObject PauseMenuUI;

    void Update()
    {
        PauseGame();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quited the Game");
    }

    public void PauseGame()
    {
        if(Ondeath.activeInHierarchy == true)
        {
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }
}
