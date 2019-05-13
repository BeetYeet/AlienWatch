using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Pathing
{

	public class Path
	{
		public Queue<Vector2> nodes;
		public float generationEndTime
		{
			get;
			private set;
		}
		public float generationStartTime
		{
			get;
			private set;
		}
		public bool finalized
		{
			get;
			private set;
		} = false;

		public Path( Queue<Vector2> nodes, float generationEndTime )
		{
			this.nodes = nodes;
			this.generationEndTime = generationEndTime;
		}

		public Path( Queue<Vector2> nodes, float generationEndTime, float generationStartTime ) : this( nodes, generationEndTime )
		{
			this.generationStartTime = generationStartTime;
			finalized = true;
		}
		public Path( float generationStartTime )
		{
			this.generationStartTime = generationStartTime;
		}

		public void Finalize( Queue<Vector2> nodes )
		{
			this.nodes = nodes;
			generationEndTime = Time.time;
			finalized = true;
		}
	}

	public abstract class Pathfinder
	{
		public int ticksPerPath;
		public int ticksSincePath;
		internal Transform origin;
		internal Transform target;
		public bool pathActive;
		public bool active;

		public Path path
		{
			get;
			internal set;
		}

		public Pathfinder( Transform origin, Transform target, int ticksPerPath )
		{
			this.origin = origin;
			this.target = target;
			this.ticksPerPath = ticksPerPath;
			ticksSincePath = Random.Range( 0, ticksPerPath );
			GameController.curr.Tick += Tick;
			Queue<Vector2> _ = new Queue<Vector2>();
			path = new Path( _, Time.time, Time.time );
		}

		public void Tick()
		{
			if ( !active )
			{
				return;
			}
			ticksSincePath++;
			if ( ticksSincePath >= ticksPerPath )
			{
				ticksSincePath -= ticksPerPath;
				GenerateNewPath();
				ticksSincePath += Random.Range( 0, 2 );
			}
		}

		internal abstract void GenerateNewPath();

		public Vector2 GetMovementVector( float travelDistance )
		{
			Vector2 orig = origin.position;
			if ( path.nodes.Count == 0 )
			{
				if ( GameController.curr.debugPaths )
					Debug.Log( "No Path" );
				return orig;
			}


			if ( path.nodes.Peek() == orig )
				GenerateNewPath();

			if ( path.nodes.Peek() == orig )
				return orig;

			Vector2 currpos = orig;

			Vector2Int pos = GameController.ClampToGrid( orig );
			if ( !GameController.curr.pathGrid.cells[pos.x, pos.y].traversable )
			{
				// currently inside wall
				for ( int x = -1; x <= 1; x++ )
				{
					for ( int y = -1; y <= 1; y++ )
					{
						if ( y == 0 && x == 0 )
						{
							continue;
						}
						int tryX = pos.x + x;
						int tryY = pos.y + y;
						GameController.PathGrid grid = GameController.curr.pathGrid;
						if ( tryX >= 0 && tryY >= 0 && tryX < grid.width && tryY < grid.height )
						{
							// valid neighbor
							GameController.PathGrid.Cell c = grid.cells[tryX, tryY];
							if ( c.traversable )
							{
								float diff = ( c.globalPos - currpos ).magnitude;
								travelDistance -= diff;
								currpos = c.globalPos;
								Debug.Log( "Nudged enemy" );
							}
							else
							{
								Debug.Log( "Failed to nudge enemy" );
							}
						}
					}
				}
			}

			/*if ( ( path.nodes.Peek() - currpos ).sqrMagnitude < .125f )
			{
				//node is very close
				path.nodes.Dequeue();
			}*/
			while ( travelDistance > 0f && path.nodes.Count != 0 )
			{
				Vector2 nextPos = path.nodes.Peek();
				float nextDist = Vector2.Distance( currpos, nextPos );
				if ( nextDist <= travelDistance )
				{
					travelDistance -= nextDist;
					currpos = nextPos;
					path.nodes.Dequeue();
				}
				else // nextDist > travelDistance
				{
					float travelPart = travelDistance / nextDist;
					currpos = Vector2.Lerp( currpos, nextPos, travelPart );
					travelDistance = 0f;
				}
			}
			return currpos;
		}
	}
	public enum PathfinderType
	{
		Straight,
		RayStretcher,
		AStar
	}
}
