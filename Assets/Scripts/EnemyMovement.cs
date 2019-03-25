﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement: MovementBaseClass
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



	void Update()
	{
		movementVelocity = Vector2.zero;
		if ( ( PlayerBaseClass.current.transform.position - transform.position ).sqrMagnitude < agroDistance * agroDistance )
			agro = true;
		if ( agro == true && Alive == true )
		{
			transform.LookAt( target.position );
			transform.Rotate( new Vector3( 0, -90, 0 ), Space.Self );

			if ( Vector3.Distance( transform.position, target.position ) > range )
			{
				movementVelocity = pathfinder.GetMovementVector( speed * Time.deltaTime ).normalized * speed;
				attack = false;
			}
			else
			{
				attack = true;
			}
		}
	}
}

