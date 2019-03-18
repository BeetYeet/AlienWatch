using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlood: MonoBehaviour
{
	public List<GameObject> bloodSplats;
	public List<GameObject> bloodSplatInstances;
	public int delayedSplats;
	public int minSplats;
	public int maxSplats;
	public float posDeviation = 0.2f;
	EnemyHealth health;
	void Start()
	{
		health = GetComponent<EnemyHealth>();
		health.OnDeath += Trigger;
	}

	void Trigger()
	{
		for ( int i = 0; i < Random.Range( minSplats, maxSplats ); i++ )
		{
			NewSplat();
		}
	}

	public void NewSplat()
	{
		Vector2 newPos = new Vector2( Random.Range( -posDeviation, posDeviation ), Random.Range( -posDeviation, posDeviation ) );
		newPos *= new Vector2( Mathf.Abs( newPos.x ), Mathf.Abs( newPos.x ) ).normalized;
		bloodSplatInstances.Add( Instantiate( bloodSplats[Random.Range( 0, bloodSplats.Count - 1 )], transform.position + HelperClass.V2toV3( newPos ), Quaternion.identity, transform ) );
	}
}
