using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseClass: MonoBehaviour
{
	public static PlayerBaseClass current
	{
		get; private set;
	}
	public PlayerMovement playerMovement
	{
		get; private set;
	}
	public PlayerMelee playerMelee
	{
		get; private set;
	}
	public PlayerMana playerMana
	{
		get; private set;
	}
	public Rigidbody2D manualRigidBody
	{
		get; private set;
	}
	public PlayerHeath playerHealth
	{
		get; private set;
	}
	public GrenadeLauncher playerGrenades
	{
		get; private set;
	}
	public PlayerSpriteChanger playerSpriteChanger
	{
		get; private set;
	}


	void Awake()
	{
		RegisterCurrentPlayer();
		GetPlayerScripts();
	}

	private void GetPlayerScripts()
	{
		playerMovement = GetComponent<PlayerMovement>();
		manualRigidBody = GetComponent<Rigidbody2D>();
		playerMelee = GetComponent<PlayerMelee>();
		playerMana = GetComponent<PlayerMana>();
		playerHealth = GetComponent<PlayerHeath>();
		playerGrenades = GetComponent<GrenadeLauncher>();
		playerSpriteChanger = GetComponent<PlayerSpriteChanger>();
	}

	private void RegisterCurrentPlayer()
	{
		if ( current != null )
		{
			throw new Exception( "Can't have 2 players!" );
		}
		current = this;
	}

	void Update()
	{

	}
}
