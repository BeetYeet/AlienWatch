using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashHandler: MonoBehaviour
{
	public int DashDamage = 10;
	public bool enabled;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if ( enabled )
		{
			transform.localEulerAngles = new Vector3( 0f, 0f, PlayerMelee.GetRawRotation( PlayerBaseClass.current.playerMovement.dashDirection ) );
		}
		else
		{
			transform.localEulerAngles = new Vector3( 0f, 0f, PlayerMelee.GetRawRotation( PlayerBaseClass.current.playerMovement.lastValidDirection ) );
		}
	}

	private void OnTriggerEnter2D( Collider2D collision )
	{
		if ( enabled )
		{
			Damageble _ = collision.GetComponent<Damageble>();
			if ( _ != null )
			{
				_.DoDamage( new DamageInfo( Faction.Player, DashDamage ) );
			}
		}
	}
}
