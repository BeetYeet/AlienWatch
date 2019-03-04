using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth: Damageble
{

	public event System.Action OnDeath;

	void Start()
	{
		OnDeath += EnemyHealth_OnDeath;
	}

	private void EnemyHealth_OnDeath()
	{
		Debug.Log( "Enemy Died!" );
	}

	void Update()
	{

	}
	public override void DoDamage( DamageInfo info )
	{
		if ( info.faction == Faction.Player )
		{
			health -= info.damage;
			if ( health < 0 )
			{
				health = 0;
			}
		}
	}


}
