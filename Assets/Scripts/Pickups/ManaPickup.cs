using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : TimeToPick
{
	float time;
	InventoryInfo _inventoryInfo;

	private void Start()
	{
		time = timeUntillPickup;

		GameObject go = GameObject.FindGameObjectWithTag("GameController");

		_inventoryInfo = go.GetComponent<InventoryInfo>();
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		time -= Time.deltaTime;
		if (time <= 0f)
		{
			_inventoryInfo.TryToAddAmount("ManaPickup", (uint)1, (x) =>
			{
				_inventoryInfo.AddEffect(2, null, () => { PlayerBaseClass.current.playerMana.mana += 50 / 16; }, null);


			});
			Destroy(gameObject);
		}
	}

}




