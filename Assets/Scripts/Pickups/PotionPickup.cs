using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPickup: MonoBehaviour
{
	float time = 1f;
	InventoryInfo inventoryInfo;
	public string potionName;

	private void Start()
	{
		inventoryInfo = GameController.curr.GetComponent<InventoryInfo>();
	}

	private void OnTriggerStay2D( Collider2D collision )
	{
		time -= Time.deltaTime;
		if ( time <= 0f )
		{
			inventoryInfo.AddPremadePotion( potionName, 1 );
			Destroy( gameObject );
		}
	}

}




