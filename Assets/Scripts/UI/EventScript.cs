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

	public void OnDeath()
	{
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
