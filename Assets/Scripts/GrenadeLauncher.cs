using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher: MonoBehaviour
{

	public Transform FirePoint;
	public GameObject grenadePrefab;
	public int manaPerGrenade = 10;
	void Update()
	{
		if ( InputHandler.current.GetWithName( "Grenade" ).GetButtonDown() )
		{
			if ( PlayerBaseClass.current.playerMana.mana >= manaPerGrenade )
				Shoot();
			PlayerBaseClass.current.playerMana.mana -= manaPerGrenade;
		}
	}

	void Shoot()
	{
		Instantiate( grenadePrefab, FirePoint.position, transform.rotation );
	}
}
