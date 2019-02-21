using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSkript : MonoBehaviour
{
  
    
    public GameObject main;
    
    public GameObject credd;

    private void Start()
    {
        main.SetActive(true);
        credd.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    //-------------------------------------------
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        main.SetActive(false);
        credd.SetActive(true);
    }

    public void BackCred()
    {
        main.SetActive(true);
        credd.SetActive(false);
    }
}
