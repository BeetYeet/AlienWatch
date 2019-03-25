using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup: MonoBehaviour
{
	PlayerHeath playerHealth;

	public int healthIncrease = 10;

	private void Awake()
	{
		playerHealth = FindObjectOfType<PlayerHeath>();
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.tag != "Player" )
			return;
		if ( playerHealth.health < playerHealth.healthMax )
		{
			playerHealth.health += healthIncrease;
			if ( playerHealth.health > playerHealth.healthMax )
				playerHealth.health = playerHealth.healthMax;
			Destroy( gameObject );
		}
	}
}
