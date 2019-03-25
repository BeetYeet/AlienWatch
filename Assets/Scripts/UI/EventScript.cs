using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventScript: MonoBehaviour
{
	public static bool GameIsPaused = false;
	public GameObject onDeath;
	public GameObject PauseMenuUI;
    public EventSystem system;


    public GameObject DeathButton;
    public GameObject PauseButton;

	private void Start()
	{
		GameController.curr.OnPlayerDeath += OnDeath;
	}

	void Update()
	{
		PauseGame();
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			if ( GameIsPaused )
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

    private void FixedUpdate()
    {
        if(onDeath.activeInHierarchy == true)
        {
            system.firstSelectedGameObject = DeathButton;
        }

        if(PauseMenuUI.activeInHierarchy == true)
        {
            system.firstSelectedGameObject = PauseButton;
        }
    }

    public void OnDeath()
	{
        system.firstSelectedGameObject = DeathButton;
		onDeath.SetActive( true );
	}
	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene( 0 );
	}

	public void QuitGame()
	{
		Application.Quit();
		Debug.Log( "Quited the Game" );
	}

	public void PauseGame()
	{
        system.firstSelectedGameObject = PauseButton;
		if ( onDeath.activeInHierarchy == true )
		{
			Time.timeScale = 0f;
		}
	}

	public void Resume()
	{
		PauseMenuUI.SetActive( false );
		Time.timeScale = 1.0f;
		GameIsPaused = false;
	}

	public void Pause()
	{
		PauseMenuUI.SetActive( true );
		Time.timeScale = 0.0f;
		GameIsPaused = true;
	}

    
}
