using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostPickup : TimeToPick
{
	float time;
	InventoryInfo _inventoryInfo;
	PlayerMelee _playerMelee;


	private void Start()
	{
		time = timeUntillPickup;
		GameObject go = GameObject.FindGameObjectWithTag("GameController");
		_inventoryInfo = go.GetComponent<InventoryInfo>();
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		_playerMelee = player.GetComponent<PlayerMelee>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		time -= Time.deltaTime;
		if (time <= 0f)
		{

			_inventoryInfo.TryToAddAmount("DamageBoostPickup", (uint)1, (x) =>
			{
				_inventoryInfo.AddEffect(30f, () =>
				{
					_playerMelee.damage += _inventoryInfo.damageModification;
					_playerMelee.postMeleeCooldown *= 1.5f; _playerMelee.meleeTime *= 1.5f;
				}, null, () => { _playerMelee.damage -= _inventoryInfo.damageModification; _playerMelee.meleeTime /= 1.5f; _playerMelee.postMeleeCooldown /= 1.5f; });

			});

			Destroy(this.gameObject);
		}


	}


}
