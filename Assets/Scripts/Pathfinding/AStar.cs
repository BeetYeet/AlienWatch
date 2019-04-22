using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathing
{
	public class AStar: Pathfinder
	{
		public AStar( Transform origin, Transform target, int ticksPerPath ) : base( origin, target, ticksPerPath )
		{

		}

		internal override void GenerateNewPath()
		{
			Queue<Vector2> nodes = new Queue<Vector2>();
			nodes.Enqueue( new Vector2( target.position.x, target.position.y ) );
			path = new Path( nodes, Time.time, Time.time );
		}
	}
}
