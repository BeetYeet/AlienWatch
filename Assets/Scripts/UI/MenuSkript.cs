using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSkript : MonoBehaviour
{


    public GameObject main;
    public GameObject Options;
    public GameObject credd;
    public GameObject howToPlay;
    public float targetTimeScale;
    public float NotMainTimeScale;
    public float changeFactor = .2f;

    private void Start()
    {
        main.SetActive(true);
        credd.SetActive(false);
    }

    private void Update()
    {
        Time.timeScale = FloatLerp(Time.timeScale, targetTimeScale, changeFactor);
        if (credd.activeInHierarchy == false)
            targetTimeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.LogError("fuck you, Mate!");
    }
    //-------------------------------------------
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Credits()
    {
        // ACTIVATES MAINMENU
        main.SetActive(false);
        // ACTIVATES CREDDITS
        credd.SetActive(true);
        targetTimeScale = NotMainTimeScale;

    }

    float FloatLerp(float a, float b, float v)
    {
        float diff = b - a;
        return a + diff * v;
    }

    public void BackCred()
    {
        main.SetActive(true);
        targetTimeScale = 1f;
        credd.SetActive(false);
        howToPlay.SetActive(false);

    }

    public void BackMain()
    {
        main.SetActive(true);
        targetTimeScale = 1f;
        credd.SetActive(false);
        Options.SetActive(false);
        howToPlay.SetActive(false);

    }

    public void Option()
    {
        // ACTIVATES MAINMENU
        main.SetActive(false);
        // ACTIVATES CREDDITS
        credd.SetActive(false);
        Options.SetActive(true);
        howToPlay.SetActive(false);

        targetTimeScale = NotMainTimeScale;

    }

    public void EnableHowToPlay()
    {
        main.SetActive(false);
        // ACTIVATES CREDDITS
        credd.SetActive(false);
        Options.SetActive(false);
        howToPlay.SetActive(true);
    }
}
