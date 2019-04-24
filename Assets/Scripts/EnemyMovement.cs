using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement: MovementBaseClass
{
	private Pathing.Pathfinder pathfinder;
	public Pathing.PathfinderType pathfinderType = Pathing.PathfinderType.Straight;
	public LayerMask wallMask;
	public int ticksPerPath = 2;
	public Transform target;
	private EnemyHealth health;
	public float pathFindRaduis = 1f;
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
		else
		if ( pathfinderType == Pathing.PathfinderType.AStar )
		{
			pathfinder = new Pathing.AStar( transform, target, ticksPerPath, wallMask );
		}
	}

	private void OnDrawGizmosSelected()
	{
		if ( pathfinder == null || pathfinder.path == null || pathfinder.path.nodes == null || pathfinder.path.nodes.Count == 0 )
			return;
		Gizmos.color = Color.yellow;
		Vector2 prev = transform.position;
		foreach ( Vector2 pos in pathfinder.path.nodes )
		{
			Gizmos.DrawLine( prev, pos );
			prev = pos;
		}
	}

	void Update()
	{
		movementVelocity = Vector2.zero;
		if ( !agro && ( PlayerBaseClass.current.transform.position - transform.position ).sqrMagnitude < agroDistance * agroDistance )
			agro = true;
		pathfinder.active = false;
		if ( agro == true && Alive == true )
		{
			pathfinder.active = true;

			if ( Vector3.Distance( transform.position, target.position ) > range * 3f / 4f )
			{
				transform.position = pathfinder.GetMovementVector( speed * Time.deltaTime );
				attack = false;
			}
			else
			{
				attack = true;
			}
		}
	}
}

