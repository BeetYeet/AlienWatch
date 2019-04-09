using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage: MonoBehaviour
{
	private bool active;

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !active )
			return;
		Damageble damageble = collision.gameObject.GetComponent<Damageble>();
		if ( damageble == null )
		{
			return;
		}
		DamageInfo damageInfo = new DamageInfo( Faction.ToEnemy, PlayerBaseClass.current.playerMelee.damage );
		damageble.DoDamage( damageInfo );

		EnemyMovement movement = collision.gameObject.GetComponent<EnemyMovement>();
		if ( movement != null )
		{
			movement.DoKnockback( HelperClass.V3toV2( collision.transform.position - PlayerBaseClass.current.transform.position ).normalized * PlayerBaseClass.current.playerMelee.strength );
		}
	}

	public void StartSwing()
	{
		active = true;
	}

	public void EndSwing()
	{
		active = false;
	}
}
