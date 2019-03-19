using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement: MonoBehaviour
{
	private Pathing.Pathfinder pathfinder;
	public Pathing.PathfinderType pathfinderType = Pathing.PathfinderType.Straight;
	public LayerMask wallMask;
	public int ticksPerPath = 2;
	private Transform target;
	private EnemyHealth health;
	public float speed;
	public float speedVariance;
	public float range;
	public bool agro = false;
	public float agroDistance = 5f;
	public bool Alive
	{
		get
		{
			return health.health > 0;
		}
	}
	public bool attack = false;
	public Vector2 knockBackVelocity = Vector2.zero;
	public float knockBackDecay = 1.1f;
	public float knockbackWeight = 1f;
	public Rigidbody2D rb;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		health = GetComponent<EnemyHealth>();
		target = PlayerBaseClass.current.transform;
		speed += Random.Range( -speedVariance * speed, speedVariance * speed );
		if ( pathfinderType == Pathing.PathfinderType.Straight )
		{
			pathfinder = new Pathing.Straight( transform, target, ticksPerPath );
		}
		else
		if ( pathfinderType == Pathing.PathfinderType.RayStretcher )
		{
			pathfinder = new Pathing.RayStretcher( transform, target, ticksPerPath, wallMask, 30f, 0, 90f );
		}
	}

	private void FixedUpdate()
	{
		knockBackVelocity /= knockBackDecay;
	}

	public void DoKnockback( Vector2 vector )
	{
		knockBackVelocity += vector / knockbackWeight;
	}

	void Update()
	{
		if ( ( PlayerBaseClass.current.transform.position - transform.position ).sqrMagnitude < agroDistance * agroDistance )
			agro = true;
		rb.velocity = knockBackVelocity;
		if ( agro == true && Alive == true )
		{
			transform.LookAt( target.position );
			transform.Rotate( new Vector3( 0, -90, 0 ), Space.Self );

			if ( Vector3.Distance( transform.position, target.position ) > range )
			{
				rb.velocity += pathfinder.GetMovementVector( speed * Time.deltaTime );
				attack = false;
			}
			else
			{
				attack = true;
			}
		}
	}
}

