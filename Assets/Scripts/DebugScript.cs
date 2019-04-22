using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugScript: MonoBehaviour
{

	void Update()
	{
		if ( Input.GetKeyDown( KeyCode.R ) )
		{
			Time.timeScale = 1f;
			SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
		}
	}
}
