using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee: MonoBehaviour
{
	PlayerBaseClass player;
	public float meleeTime = .2f;
	public float meleeTimeLeft = 0f;
	public bool isMeleeing
	{
		get
		{
			return meleeTimeLeft != 0f;
		}
	}

	void Start()
	{
		player = PlayerBaseClass.current;
	}

	void Update()
	{
		if ( ( Input.GetButtonDown( "Fire1" ) || Input.GetKeyDown( KeyCode.LeftAlt ) ) && player.playerMovement.movementState != PlayerMovement.PlayerMovementState.Fixed && !isMeleeing )
		{
			Debug.Log( "Did Melee" );
			meleeTimeLeft += meleeTime;
			player.playerMovement.TriggerFixed( meleeTime );
		}
		if ( meleeTimeLeft > 0f )
		{
			meleeTimeLeft -= Time.deltaTime;
			if ( meleeTimeLeft < 0 )
			{
				meleeTimeLeft = 0f;
			}
			else if ( meleeTimeLeft > 0f )
			{
				DoMeleeDamage();
			}

		}
	}

	private void DoMeleeDamage()
	{
		Debug.Log( "Did Melee" );
		//TODO: melee damage
	}
}
