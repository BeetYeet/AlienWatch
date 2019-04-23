using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
			double time;
			List<Vector2> nodes = GameController.curr.PathfindAstar( origin.position, target.position, out time );
			Queue<Vector2> fin = new Queue<Vector2>();
			nodes.ForEach( ( x ) =>{fin.Enqueue( x );} );
			Path newPath = new Path( fin, Time.time + (float) time / 1000f, Time.time );
			path = newPath;
		}
	}
}
