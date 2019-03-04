using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage: MonoBehaviour
{
	public int damage;
	private bool active;

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( !active )
			return;
		DamageInfo damageInfo = new DamageInfo(Faction.Player, damage );
		collision.gameObject.GetComponent<Damageble>().DoDamage( damageInfo );
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
