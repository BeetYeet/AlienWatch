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
		if ( damageble == null ) {
			return;
		}
		DamageInfo damageInfo = new DamageInfo(Faction.Player, PlayerBaseClass.current.playerMelee.damage );
		damageble.DoDamage( damageInfo );
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
