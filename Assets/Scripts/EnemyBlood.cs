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
	public float velDeviation = 0.5f;
	EnemyHealth health;
	bool dead = false;
	public int ticksPerNextSplat = 1;
	public int ticksSoFar = 0;
	public float increaseFactor = 1.2f;

	void Start()
	{
		health = GetComponent<EnemyHealth>();
		health.OnDeath += Trigger;
		GameController.curr.Tick += Tick;
	}

	void Trigger()
	{
		int numSplats = Random.Range( minSplats, maxSplats );
		for ( int i = 0; i < numSplats; i++ )
		{
			NewSplatInitial();
		}
		dead = true;
	}

	public void Tick()
	{
		if ( !dead )
			return;
		ticksSoFar++;
		if ( ticksSoFar >= ticksPerNextSplat )
		{
			NewSplatContinued();
			ticksSoFar -= ticksPerNextSplat;
			int newTicks = (int) ( ticksPerNextSplat * increaseFactor );
			if ( newTicks <= ticksPerNextSplat )
			{
				newTicks = ticksPerNextSplat + 1;
			}
			ticksPerNextSplat = newTicks;
		}
	}

	private void NewSplatContinued()
	{
		GameObject obj = GenSplat( RandVector( posDeviation / 5f ) );
		bloodSplatInstances.Add( obj );
		Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
		rb.velocity = RandVector( velDeviation ) / 5f;
	}

	public void NewSplatInitial()
	{
		GameObject obj = GenSplat( RandVector( posDeviation ) );
		bloodSplatInstances.Add( obj );
		Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
		rb.velocity = RandVector( posDeviation );
	}

	public Vector2 RandVector( float max )
	{
		Vector2 vector = new Vector2( Random.Range( -max, max ), Random.Range( -posDeviation, posDeviation ) );
		vector *= new Vector2( Mathf.Abs( vector.x ), Mathf.Abs( vector.y ) ).normalized;
		return vector;
	}

	private GameObject GenSplat( Vector2 pos )
	{
		return Instantiate( bloodSplats[Random.Range( 0, bloodSplats.Count - 1 )], transform.position + HelperClass.V2toV3( pos ), Quaternion.identity, transform );
	}
}
