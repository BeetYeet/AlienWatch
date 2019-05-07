using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Damageble
{
	public GameObject UnaDestroyParticle;
	public Sprite Una, DestroyedUna;


	SpriteRenderer CurrentSprite;
	private void Start()
	{
		CurrentSprite = GetComponent<SpriteRenderer>();
		GameController.curr.ChangeTraversable( GameController.ClampToGrid(transform.position), false);
	}

	public override void DoDamage(DamageInfo info)
	{
		TriggerDestroy(UnaDestroyParticle, DestroyedUna);
	}

	private void TriggerDestroy(GameObject Particle, Sprite DestroySprite)
	{
		Instantiate(Particle, transform.position, Quaternion.identity);
		CurrentSprite.sprite = DestroySprite;
		Destroy(this);
	}
}
