using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade: MonoBehaviour
{
	public GameObject explotionPrefab;
	public float timeToExplode = 2f;
	public float speed = 20f;
	private Rigidbody2D bulletRB;
	// Start is called before the first frame update
	void Start()
	{
		bulletRB = GetComponent<Rigidbody2D>();
		Vector2 v;
		if ( PlayerBaseClass.current.playerMovement.playerDir != PlayerMovement.PlayerDirection.None )
		{
			v = PlayerMovement.GetVectorDirection( PlayerBaseClass.current.playerMovement.playerDir );
		}
		else
		{
			v = PlayerMovement.GetVectorDirection( PlayerBaseClass.current.playerMovement.lastValidDirection )/5f;
		}
		bulletRB.velocity = v * speed;
		//transform.up * speed;
	}

	private void Update()
	{
		timeToExplode -= Time.deltaTime;

		if ( timeToExplode <= 0f )
		{
			Destroy( gameObject );
			Instantiate( explotionPrefab, transform.position, Quaternion.identity );
		}
	}
}
//rigidbody.velocity = PlayerMovement.GetVectorDirection(PlayerBaseClass.current.playerMovement.playerDir) * grenadeVelocity;