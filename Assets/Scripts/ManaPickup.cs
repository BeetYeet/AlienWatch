using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup: MonoBehaviour
{
	PlayerMana playerMana;
	public int manaIncrease = 20;

	private void Start()
	{
		playerMana = PlayerBaseClass.current.playerMana;
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( collision.tag != "Player" )
			return;
		if ( playerMana.mana == playerMana.maxMana )
			return;
		playerMana.mana += manaIncrease;
		if ( playerMana.mana > playerMana.maxMana )
			playerMana.mana = playerMana.maxMana;
		Destroy( gameObject );
	}



}
