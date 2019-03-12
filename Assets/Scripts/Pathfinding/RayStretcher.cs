using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Unity.Jobs;

namespace Pathing
{
	public class RayStretcher: Pathfinder
	{
		private LayerMask rayMask;
		public float raySpacing
		{
			get;
			private set;
		}
		public int maxLevelDepth
		{
			get;
			private set;
		}
		public float rayArc
		{
			get;
			private set;
		}
		private bool hybrid = false;
		private float hybridDistance;


		public List<Ray2D> rayList = new List<Ray2D>();

		public void DrawRays()
		{
			rayList.ForEach( ( x ) => { Debug.DrawRay( x.origin, x.direction, Color.cyan, .25f ); } );
		}

		/// <summary>
		/// Draws a ray and strecthes it find a path to the target even when separated by walls
		/// </summary>
		/// <param name="target"></param>
		/// <param name="ticksPerPath"></param>
		public RayStretcher( Transform origin, Transform target, int ticksPerPath, LayerMask rayMask, float raySpacing, int maxLevelDepth, float rayArc ) : base( origin, target, ticksPerPath )
		{
			this.rayMask = rayMask;
			this.raySpacing = raySpacing;
			this.maxLevelDepth = maxLevelDepth;
			this.rayArc = rayArc;
			Init();
		}

		private void Init()
		{
			GameController.curr.Tick += DrawRays;
		}

		/// <summary>
		/// Like the normal RayStretcher, but pathfinds using the Straight pathfinder when within hybridDistance of the target
		/// </summary>
		/// <param name="target"></param>
		/// <param name="ticksPerPath"></param>
		/// <param name="hybridDistance"></param>
		public RayStretcher( Transform origin, Transform target, int ticksPerPath, LayerMask rayMask, float raySpacing, int maxLevelDepth, float rayArc, float hybridDistance ) : base( origin, target, ticksPerPath )
		{
			hybrid = true;
			this.hybridDistance = hybridDistance;

			this.rayMask = rayMask;
			this.raySpacing = raySpacing;
			this.maxLevelDepth = maxLevelDepth;
			this.rayArc = rayArc;
			Init();
		}


		internal override void GenerateNewPath()
		{
			GenerateNewPath_Async( HelperClass.V3toV2( origin.position ), HelperClass.V3toV2( target.position ), rayList, maxLevelDepth, rayMask );
		}

		public void ReturnPath( Path newPath )
		{


			if ( newPath == null )
			{
				// no Path at all
				Debug.Log( "Path failed initial null check" );
				return;
			}
			if ( !newPath.finalized )
			{
				// if the path wasn't finalized
				Debug.Log( "Path not finalized" );
				return;
			}
			if ( Time.time - newPath.generationStartTime > ( ticksPerPath / ( GameController.curr.ticksPerMinute / 60 ) ) * .8f )
			{
				// if it took more than 80% of the refresh time
				Debug.Log( "Path failed initial null check" );
				return;
			}
			if ( path.generationEndTime > newPath.generationEndTime )
			{
				//the current path is more recent
				Debug.Log( "Path too old" );
				return;
			}
			if ( newPath.nodes == null )
			{
				// no Queue at all
				Debug.Log( "Path failed final null check" );
				return;
			}
			if ( newPath.nodes.Count == 0 )
			{
				// no nodes in the path
				Debug.Log( "Path empty" );
				return;
			}

			Debug.Log( "Path validated" );

			Debug.Log( "Total computaion time:\t" + ( ( newPath.generationEndTime - newPath.generationStartTime ) / 1000f ).ToString() + " milliseconds" );
			path = newPath;
		}

		internal async void GenerateNewPath_Async( Vector2 origin, Vector2 target, List<Ray2D> rayList, int maxLevelDepth, LayerMask rayMask )
		{
			float startTime = Time.time;
			Debug.Log( "Started computing path at world time:\t" + startTime.ToString() );
			Task<Path> task = new Task<Path>( () => GenerateNewPath_Sync( startTime, origin, target, rayList, maxLevelDepth, rayMask ) );
			task.Start();
			Path path = await task;
			if ( path.finalized )
			{
				ReturnPath( path );
				Debug.Log( "Finalized and returned path" );
			}
			else
			{
				Debug.Log( "Unfinalized path" );
			}
		}


		internal Path GenerateNewPath_Sync( float startTime, Vector2 origin, Vector2 target, List<Ray2D> rayList, int maxLevelDepth, LayerMask rayMask )
		{
			Path newPath = new Path( startTime );
			rayList = new List<Ray2D>();
			if ( hybrid && Vector2.SqrMagnitude( HelperClass.V3toV2( target ) - HelperClass.V3toV2( origin ) ) < hybridDistance * hybridDistance )
			{
				Queue<Vector2> nodes = new Queue<Vector2>();
				rayList.Add( new Ray2D( origin, target ) );
				nodes.Enqueue( target );
				newPath.Finalize( nodes );
			}
			else
			{
				RayFan rayFan = new RayFan( maxLevelDepth, this, origin, ( target - origin ).normalized, new List<Vector2>(), rayMask );
				RayFanResult res = rayFan.Trigger();
				if ( res.result == RayFanResult.resultType.Hit )
				{
					Queue<Vector2> nodes = new Queue<Vector2>();
					res.pathSoFar.ForEach( ( x ) => { nodes.Enqueue( x ); } );
					newPath.Finalize( nodes );
				}
			}
			return newPath;
		}
	}

	public class RayFan
	{
		private int depthLeft;
		RayStretcher parent;
		private int numRays;
		private int maxRays;
		private Vector2 origin;
		private Vector2 straightDirection;
		List<Vector2> pathYet;
		private LayerMask rayMask;


		public RayFan( int depthLeft, RayStretcher parent, Vector2 origin, Vector2 straightDirection, List<Vector2> pathYet, LayerMask rayMask )
		{
			this.depthLeft = depthLeft;
			this.parent = parent;
			this.origin = origin;
			this.straightDirection = straightDirection;
			this.pathYet = pathYet;
			this.rayMask = rayMask;

			maxRays = (int) ( parent.rayArc * 2f / parent.raySpacing );
		}

		public RayFanResult Trigger()
		{
			List<NextRayFanData> subFans = new List<NextRayFanData>();
			RayFanResult lastReturn = new RayFanResult( null, null, RayFanResult.resultType.NoHit );
			while ( lastReturn.result == RayFanResult.resultType.NoHit )
			{
				RayFanResult res = NextRay();
				if ( res != null && res.next != null )
				{
					subFans.Add( res.next );
				}
			}

			if ( lastReturn.result == RayFanResult.resultType.NoMoreRays )
			{
				if ( depthLeft > 1 )
				{
					foreach ( NextRayFanData fan in subFans )
					{
						RayFanResult res = fan.Generate().Trigger();
						if ( res.result == RayFanResult.resultType.Hit ) //we found a path
						{
							return res;
						}
						else //RayFanResult.resultType.NoMoreRays
						{
							// we couldnt find a path originating from this ray	
						}
					}
				}
				//no rays gave good results
			}

			return null;
		}

		public RayFanResult NextRay()
		{
			if ( numRays > maxRays )
			{ // oh fuck go back
				return new RayFanResult( null, null, RayFanResult.resultType.NoMoreRays );
			}

			float dir = parent.raySpacing * ( numRays / 2 );
			if ( numRays % 2 == 0 )
			{
				dir = -dir;
			}
			numRays++;
			Vector2 rayDir = HelperClass.RotateAroundAxis( straightDirection, Vector2.zero, dir );
			Ray2D ray = new Ray2D( origin, rayDir );
			RaycastHit2D hit = Physics2D.Raycast( ray.origin, ray.direction, 20f, rayMask );
			Ray2D playerRay = new Ray2D( hit.point, HelperClass.V3toV2( parent.target.position ) - hit.point );
			RaycastHit2D playerHit = Physics2D.Raycast( playerRay.origin, playerRay.direction, 200f, rayMask );

			parent.rayList.Add( ray );
			parent.rayList.Add( playerRay );

			List<Vector2> path = new List<Vector2>();
			pathYet.ForEach( ( x ) => { path.Add( x ); } );
			path.Add( hit.point );

			NextRayFanData data = new NextRayFanData( depthLeft - 1, parent, origin, hit.normal, path, rayMask );

			RayFanResult.resultType hitPlayer = RayFanResult.resultType.NoHit;
			if ( playerHit.collider.gameObject.tag == "Player" )
			{
				hitPlayer = RayFanResult.resultType.Hit;
				path.Add( HelperClass.V3toV2( parent.target.position ) );
			}


			RayFanResult res = new RayFanResult( path, data, hitPlayer );

			return null;
		}
	}

	public class NextRayFanData
	{
		private int depthLeft;
		private RayStretcher parent;
		private Vector2 origin;
		private Vector2 straightDirection;
		private List<Vector2> pathYet;
		private LayerMask rayMask;

		public NextRayFanData( int depthLeft, RayStretcher parent, Vector2 origin, Vector2 straightDirection, List<Vector2> pathYet, LayerMask rayMask )
		{
			this.depthLeft = depthLeft;
			this.parent = parent;
			this.origin = origin;
			this.straightDirection = straightDirection;
			this.pathYet = pathYet;
			this.rayMask = rayMask;
		}

		public RayFan Generate()
		{
			return new RayFan( depthLeft, parent, origin, straightDirection, pathYet, rayMask );
		}
	}

	public class RayFanResult
	{
		public List<Vector2> pathSoFar;
		public resultType result;
		public NextRayFanData next;

		public RayFanResult( List<Vector2> pathSoFar, NextRayFanData next, resultType result )
		{
			this.pathSoFar = pathSoFar;
			this.next = next;
			this.result = result;
		}

		public enum resultType
		{
			Hit,
			NoHit,
			NoMoreRays
		}


	}
}
