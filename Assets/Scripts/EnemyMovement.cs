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
		health = GetComponent<EnemyHealth>();
		target = PlayerBaseClass.current.transform;
		if ( pathfinderType == Pathing.PathfinderType.Straight )
		{
			pathfinder = new Pathing.Straight( transform, target, ticksPerPath );
		}
		else
		if ( pathfinderType == Pathing.PathfinderType.RayStretcher )
		{
			pathfinder = new Pathing.RayStretcher( transform, target, ticksPerPath, wallMask, 30f, 0, 90f );
		}
		speed += Random.Range( -speedVariance * speed, speedVariance * speed );
	}

	void Update()
	{
		if ( agro == true && Alive == true )
		{
			transform.LookAt( target.position );
			transform.Rotate( new Vector3( 0, -90, 0 ), Space.Self );

			if ( Vector3.Distance( transform.position, target.position ) > range )
			{
				Vector2 nextPos = pathfinder.GetNextPosition( speed * Time.deltaTime );
				transform.position = new Vector3( nextPos.x, nextPos.y, 0f );
				attack = false;
			}
			else
			{
				attack = true;
			}
		}
	}
}

