using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	public int sceneToLoad = 0;
	public void Trigger()
	{
		SceneManager.LoadScene( sceneToLoad );
	}
}
