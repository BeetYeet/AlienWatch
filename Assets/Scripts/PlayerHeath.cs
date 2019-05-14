using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath: Damageble
{
	public int healthMax = 100;
	public bool dead
	{
		get
		{
			return health == 0;
		}
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if ( health == 0 )
		{
			//die
		}
		if ( health > healthMax )
		{
			health = healthMax;
		}
	}
	public override void DoDamage( DamageInfo info )
	{
		if ( PlayerBaseClass.current.playerMovement.movementState == PlayerMovement.PlayerMovementState.Dashing )
		{
			return;
		}
		base.DoDamage( info );
		if ( info.faction == Faction.ToPlayer )
		{
			health -= info.damage;
			if ( health < 0 )
			{
				health = 0;
			}
		}
	}
}

public struct DamageInfo
{
	public Faction faction;
	public int damage;
	public DamageInfo( Faction faction, int damage )
	{
		this.faction = faction;
		this.damage = damage;
	}
}
public enum Faction
{
	ToEnemy,
	ToPlayer
}
