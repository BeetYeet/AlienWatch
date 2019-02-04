using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventScript : MonoBehaviour
{
    public GameObject Ondeath;

    void Update()
    {
        PauseGame();
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
}
