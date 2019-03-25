using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlood: MonoBehaviour
{
	public List<GameObject> bloodSplats;
	public List<GameObject> bloodSplatInstances;
	private Rigidbody2D rb;
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
	public float velocityBloodFactor = 1f;
	public static Transform bloodParent;

	void Start()
	{
		health = GetComponent<EnemyHealth>();
		health.OnDeath += Trigger;
		GameController.curr.Tick += Tick;
		rb = GetComponent<Rigidbody2D>();
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

	public bool PercentChance( float trueChance )
	{
		if ( trueChance >= 1f )
		{
			return true;
		}
		if ( trueChance <= 0f )
		{
			return false;
		}
		float num = Random.Range( 0f, 1f );
		return num < trueChance;
	}

	private void Update()
	{
		if ( !dead )
			return;
		if ( PercentChance( rb.velocity.magnitude * velocityBloodFactor * Time.deltaTime ) )
			NewSplatVelocity();

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
		Vector2 _ = RandVector( posDeviation / 2f );
		GameObject obj = GenSplat( _ );
		bloodSplatInstances.Add( obj );
		Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
		rb.velocity = _;
	}

	private void NewSplatVelocity()
	{
		Vector2 _ = RandVector( posDeviation / 4f );
		GameObject obj = GenSplat( _ );
		bloodSplatInstances.Add( obj );
		Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
		rb.velocity = _ + rb.velocity / 2;
		obj.transform.localScale = Vector3.one / 2f;
	}

	public void NewSplatInitial()
	{
		Vector2 _ = RandVector( posDeviation );
		GameObject obj = GenSplat( _ );
		bloodSplatInstances.Add( obj );
		Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
		rb.velocity = _;
	}

	public Vector2 RandVector( float max )
	{
		Vector2 vector = new Vector2( Random.Range( -max, max ), Random.Range( -max, max ) );
		vector *= new Vector2( Mathf.Abs( vector.x ), Mathf.Abs( vector.y ) ).normalized;
		return vector;
	}

	private GameObject GenSplat( Vector2 pos )
	{
		return Instantiate( bloodSplats[Random.Range( 0, bloodSplats.Count - 1 )], transform.position + HelperClass.V2toV3( pos ), Quaternion.identity, bloodParent );
	}
}
