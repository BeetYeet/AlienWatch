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
		int quick;

		public AStar( Transform origin, Transform target, int ticksPerPath, LayerMask wallMask ) : base( origin, target, ticksPerPath )
		{
			this.wallMask = wallMask;
			quick = (int) Mathf.Ceil( ticksPerPath * ( 3f / 4f ) );
		}

		internal override void GenerateNewPath()
		{
			Queue<Vector2> fin = new Queue<Vector2>();
			double time = 0.001d;

			Vector2 diff = target.position - origin.position;
			float diffMag = diff.magnitude;

			Vector2 normDiff = diff.normalized;
			Vector2 side = new Vector2( normDiff.y, -1 * normDiff.x ) * pathWidth;

			List<Ray2D> rays = new List<Ray2D>();

			rays.Add( new Ray2D( origin.position, diff ) );
			rays.Add( new Ray2D( HelperClass.V3toV2( origin.position ) + side / 2, diff ) );
			rays.Add( new Ray2D( HelperClass.V3toV2( origin.position ) - side / 2, diff ) );

			List<RaycastHit2D> hits = new List<RaycastHit2D>();

			bool collide = false;
			foreach ( Ray2D r in rays )
			{
				RaycastHit2D h = Physics2D.Raycast( r.origin, r.direction, diffMag, wallMask );
				hits.Add( h );
				if ( GameController.DebugEnemyPathing )
					Debug.DrawLine( r.origin, h ? h.point : r.origin + r.direction * diffMag, h ? Color.red : Color.green, .25f);
				collide |= h;
			}


			if ( collide )
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
				ticksSincePath += quick;
			}


			Path newPath = new Path( fin, Time.time + (float) time / 1000f, Time.time );
			path = newPath;
		}
	}
}
