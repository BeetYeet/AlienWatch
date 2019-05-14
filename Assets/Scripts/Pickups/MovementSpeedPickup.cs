using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPickup : TimeToPick
{
	float time;
	InventoryInfo _inventoryInfo;

	private void Start()
	{
		time = timeUntillPickup;
		GameObject go = GameObject.FindGameObjectWithTag("GameController");

		_inventoryInfo = go.GetComponent<InventoryInfo>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		time -= Time.deltaTime;
		if (time <= 0f)
		{

			_inventoryInfo.TryToAddAmount("MovementSpeedPickup", (uint)1, (x) =>
			{
				_inventoryInfo.AddEffect(20f, () => { PlayerBaseClass.current.playerMovement.movementSpeed *= 1.5f; }, null, () => { PlayerBaseClass.current.playerMovement.movementSpeed /= 1.5f; });

			});
			Destroy(this.gameObject);
		}
	}
}
