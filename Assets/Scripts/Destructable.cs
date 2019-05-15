using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable: Damageble
{
	public GameObject UnaDestroyParticle;
	public Sprite DestroyedUna;


	SpriteRenderer CurrentSprite;
	private void Start()
	{
		CurrentSprite = GetComponent<SpriteRenderer>();
		GameController.curr.ChangeTraversable( GameController.ClampToGrid( transform.position ), false );
	}

	public override void DoDamage( DamageInfo info )
	{
		TriggerDestroy();
	}

	private void TriggerDestroy()
	{
		Instantiate( UnaDestroyParticle, transform.position, Quaternion.identity );
		CurrentSprite.sprite = DestroyedUna;
		GetComponent<Collider2D>().enabled = false;
		gameObject.layer = LayerMask.NameToLayer( "Terrain_Passable" );
		Destroy( this );
	}
}
