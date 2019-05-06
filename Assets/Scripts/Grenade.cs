using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade: MonoBehaviour
{
	public GameObject explotionPrefab;
    public GameObject explotionSmokePrefab; 
    public new GameObject light; 
	public float timeToExplode = 2f;
	public float speed = 20f;
	private Rigidbody2D bulletRB;
	// Start is called before the first frame update
	void Start()
	{
		bulletRB = GetComponent<Rigidbody2D>();
		Vector2 v;
		v = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
		if ( PlayerBaseClass.current.playerMovement.playerDir == PlayerMovement.PlayerDirection.None )
		{
			v = PlayerMovement.GetVectorDirection( PlayerBaseClass.current.playerMovement.lastValidDirection ) / 3f;
		}
		bulletRB.velocity = v * speed;
		//transform.up * speed;
	}

	private void Update()
	{
		timeToExplode -= Time.deltaTime;

		if ( timeToExplode <= 0f )
		{
			HelperClass.DoAOEDamage( HelperClass.V3toV2( transform.position ), 5f, 50, Faction.ToEnemy, 20f );
			Destroy( gameObject );
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
            Instantiate(light, transform.position, Quaternion.identity);
            Instantiate(explotionSmokePrefab, transform.position, Quaternion.identity);
        }
	}
}
//rigidbody.velocity = PlayerMovement.GetVectorDirection(PlayerBaseClass.current.playerMovement.playerDir) * grenadeVelocity;