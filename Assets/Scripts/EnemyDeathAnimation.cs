using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathAnimation: MonoBehaviour
{

	EnemyHealth health;
	void Start()
	{
		health = GetComponent<EnemyHealth>();
		health.OnDeath += OnDeath;
	}

	void OnDeath()
	{
		GetComponent<SpriteRenderer>().color = new Color( .5f, .2f, .2f, .7f );
		gameObject.layer = LayerMask.NameToLayer( "PlayerDashing" );
	}
}
