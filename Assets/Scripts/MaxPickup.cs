using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxPickup: MonoBehaviour
{
	PlayerHeath playerHealth;
	public int healthIncrease = 25;

	private void Start()
	{
		playerHealth = PlayerBaseClass.current.playerHealth;
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.tag != "Player" )
			return;
		playerHealth.health += healthIncrease;
		if ( playerHealth.health > playerHealth.healthMax )
			playerHealth.health = playerHealth.healthMax;
		Destroy( gameObject );
	}
}
