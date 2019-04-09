using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageble : MonoBehaviour
{
	public int health;
	public virtual void DoDamage(DamageInfo info)
	{
		DamagePopup.Create(transform.position, info.damage, GameAssets.curr.hpDownColor);
	}
}
