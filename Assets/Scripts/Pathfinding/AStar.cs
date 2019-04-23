using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pathing
{
	public class AStar: Pathfinder
	{
		LayerMask wallMask;
		float pathWidth = .7f;

		public AStar( Transform origin, Transform target, int ticksPerPath, LayerMask wallMask ) : base( origin, target, ticksPerPath )
		{
			this.wallMask = wallMask;
		}

		internal override void GenerateNewPath()
		{
			Queue<Vector2> fin = new Queue<Vector2>();
			double time = 0.001d;

			Vector2 diff = target.position - origin.position;
			Vector2 normDiff = diff.normalized;

			Vector2 side = new Vector2( normDiff.y, -1 * normDiff.x ) * pathWidth;

			RaycastHit2D hitCenter = Physics2D.Raycast( origin.position, diff, diff.magnitude, wallMask );
			RaycastHit2D hitSide1 = Physics2D.Raycast( HelperClass.V3toV2( origin.position ) + side / 2, diff, diff.magnitude, wallMask );
			RaycastHit2D hitSide2 = Physics2D.Raycast( HelperClass.V3toV2( origin.position ) - side / 2, diff, diff.magnitude, wallMask );
			if ( hitCenter )
			{
				if ( GameController.curr.debugPaths )
					Debug.Log( "No quick path found, using A*" );
				List<Vector2> nodes = GameController.curr.PathfindAstar( origin.position, target.position, out time );
				nodes.ForEach( ( x ) => { fin.Enqueue( x ); } );
			}
			else
			{
				if ( GameController.curr.debugPaths )
					Debug.Log( "Found quicker path" );
				fin.Enqueue( target.position );
				ticksSincePath += (int) Mathf.Ceil( ticksPerPath * 3f / 4f );
			}


			Path newPath = new Path( fin, Time.time + (float) time / 1000f, Time.time );
			path = newPath;
		}
	}
}
