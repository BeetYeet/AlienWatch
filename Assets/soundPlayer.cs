using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPlayer : MonoBehaviour
{
	
 

    // Update is called once per frame
    void Update()
    {
		PlayerBaseClass.current.playerMelee.SwingStart += () => 
		{
			SoundManager.PlaySound("swing");
		};
    }
}
