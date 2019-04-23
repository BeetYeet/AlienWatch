using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathing
{
	public class AStar: Pathfinder
	{
		LayerMask wallMask;


		public AStar( Transform origin, Transform target, int ticksPerPath, LayerMask wallMask ) : base( origin, target, ticksPerPath )
		{
			this.wallMask = wallMask;
		}

		internal override void GenerateNewPath()
		{
			Queue<Vector2> fin = new Queue<Vector2>();
			double time = 0.001d;

			Vector2 diff = target.position - origin.position;
			RaycastHit2D hit = Physics2D.Raycast( origin.position, diff, diff.magnitude, wallMask );
			if ( hit == true )
			{
				Debug.Log( "Found quicker path" );
				fin.Enqueue( target.position );
			}
			else
			{
				Debug.Log( "No quick path found, using A*" );
				List<Vector2> nodes = GameController.curr.PathfindAstar( origin.position, target.position, out time );
				nodes.ForEach( ( x ) => { fin.Enqueue( x ); } );
			}


			Path newPath = new Path( fin, Time.time + (float) time / 1000f, Time.time );
			path = newPath;
		}
	}
}
