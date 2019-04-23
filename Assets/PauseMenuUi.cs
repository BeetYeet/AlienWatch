using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUi : MonoBehaviour
{
	public GameObject pauseMenu;
	public GameObject OptionMenu;
	
	public void ActivateOptions()
	{
		pauseMenu.SetActive(false);
		OptionMenu.SetActive(true);
	}
	
	public void ActivatePause()
	{
		pauseMenu.SetActive(true);
		OptionMenu.SetActive(false);
	}

	public void FullscreenToggle()
	{
		Screen.fullScreen = !Screen.fullScreen;
	}
}
