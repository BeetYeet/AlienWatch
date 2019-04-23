using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageble : MonoBehaviour
{
	public int health;
	public virtual void DoDamage(DamageInfo info)
	{
		bool toPlayer = info.faction == Faction.ToPlayer;
		bool heal = info.damage < 0;
		if(health == 0)
		{
			return;
		}
		if (heal)
		{
			DamagePopup.Create(transform.position, info.damage, 3.41f, GameAssets.curr.hpUpColor);
		}
		else
		{
			if (toPlayer)
			{
				DamagePopup.Create(transform.position, info.damage, 3f, GameAssets.curr.hpDownPlayerColor);
				SoundManager.PlaySound("PlayerHit");
			}
			else
			{
				DamagePopup.Create(transform.position, info.damage, 3.41f, GameAssets.curr.hpDownColor);
				SoundManager.PlaySound("EnemyHit");
			}

		}


	}
}
