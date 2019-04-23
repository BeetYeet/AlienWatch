using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlayer : MonoBehaviour
{


	private void Start()
	{
		PlayerBaseClass.current.playerMelee.SwingStart += () =>
		{
			SoundManager.PlaySound("swing");
		};
	}
	
}
