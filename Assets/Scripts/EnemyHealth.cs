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
		//Debug.Log( "Enemy Died!" );
	}

	void Update()
	{

	}
	public override void DoDamage( DamageInfo info )
	{
		base.DoDamage(info);
		if ( info.faction == Faction.Player )
		{
			bool canDie = health > 0;
			health -= info.damage;

			if ( health <= 0 )
			{
				health = 0;
				if ( canDie )
				{
					OnDeath();
				}
			}
		}
	}


}
