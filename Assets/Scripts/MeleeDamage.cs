using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
	public Faction faction;
	public int damage;

	private void OnTriggerEnter2D( Collider2D collision )
	{
		DamageInfo di = new DamageInfo();
		di.damage = damage;
		di.faction = faction;
		collision.SendMessageUpwards("DoDamage", di, SendMessageOptions.DontRequireReceiver);
	}
}
