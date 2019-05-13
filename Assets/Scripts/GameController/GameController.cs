using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameController: MonoBehaviour
{
	public static GameController curr
	{
		get;
		private set;
	}
	public event System.Action Tick;
	private bool playerHasDied;
	public event System.Action OnPlayerDeath;

	public int ticksPerMinute = 600;
	public int ticksPerSecond
	{
		get;
		private set;
	}
	private float TickTime
	{
		get
		{
			return 60f / ticksPerMinute;
		}
	}
	private float nextTick;
	void Awake() // Awake() går innan Start()
	{
		if ( curr != null )
			throw new System.Exception( "Too many instances of GameController, should only be one" );
		curr = this;
		SoundManager.Initalize();
		if ( Walls == null )
		{
			throw new System.NullReferenceException( "We need a grid for A* pathfinding" );
		}
		else
			regeneratePathGrid();
		ticksPerSecond = (int) 60f / ticksPerMinute;
	}

	private void DoTick()
	{
		Tick?.Invoke();
		nextTick += TickTime;
	}

	private bool CheckTick()
	{
		if ( Time.time > nextTick )
		{
			DoTick();

		}
		if ( Time.time > nextTick )
		{
			return true;
		}
		return false;
	}

	void Update()
	{
		while ( CheckTick() )
			;
		if ( PlayerBaseClass.current.playerHealth.dead && !playerHasDied )
		{
			playerHasDied = true;
			OnPlayerDeath.Invoke();
		}
	}

	#region Astar

	public Tilemap Walls;
	[SerializeField]
	public PathGrid pathGrid;
	private LayerMask wallMask;

	public Color traverseColor = Color.green;
	public Color noTraverseColor = Color.red;
	public bool debugPaths = false;

	[HideInInspector]
	public static bool DebugEnemyPathing
	{
		get
		{
			return curr.debugEnemyPathing;
		}
		set
		{
			curr.debugEnemyPathing = value;
		}
	}

	public bool debugEnemyPathing = false;

	public void ChangeTraversable( Vector2Int pos, bool val )
	{
		pathGrid.cells[pos.x, pos.y].traversable = val;
	}

	public List<Vector2> PathfindAstar( Vector2 start, Vector2 end, out double time )
	{
		System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
		Vector2Int startpos = ClampToGrid( start );
		Vector2Int endpos = ClampToGrid( end );

		if (
		//!pathGrid.cells[startpos.x, startpos.y].traversable || 
		!pathGrid.cells[endpos.x, endpos.y].traversable )
		{
			Debug.LogWarning( "End cell is invalid" );
			time = 0.001d;
			return new List<Vector2>() { end };
		}
		timer.Start();
		List<Vector2Int> gridPath = PathContext.FindPath( startpos, endpos );
		timer.Stop();
		if ( curr.debugPaths )
			Debug.Log( string.Format( "Path found after {0} milliseconds with {1} nodes", timer.Elapsed.TotalMilliseconds, gridPath.Count ) );
		List<Vector2> path = new List<Vector2>();
		Vector2 direction = Vector2.zero;
		for ( int i = 0; i < gridPath.Count - 1; i++ )
		{
			Vector2Int intPos = gridPath[i];
			Vector2Int intPosPrev = gridPath[i + 1];
			Vector2 newDirection = pathGrid.cells[intPosPrev.x, intPosPrev.y].globalPos - pathGrid.cells[intPos.x, intPos.y].globalPos;
			if ( direction != newDirection )
			{
				direction = newDirection;
				path.Add( pathGrid.cells[intPos.x, intPos.y].globalPos );
			}
		}
		path.Add( pathGrid.cells[gridPath[gridPath.Count - 1].x, gridPath[gridPath.Count - 1].y].globalPos );
		time = timer.Elapsed.TotalMilliseconds;
		return path;
	}

	public static Vector2Int ClampToGrid( Vector3 pos )
	{
		Vector3Int _ = GameController.curr.Walls.WorldToCell( pos ) - new Vector3Int( GameController.curr.Walls.cellBounds.xMin, GameController.curr.Walls.cellBounds.yMin, 0 );
		return new Vector2Int( _.x, _.y );
	}

	public void OnDrawGizmosSelected()
	{
		if ( pathGrid == null || pathGrid.cells == null || pathGrid.cells.GetLength( 0 ) == 0 || pathGrid.cells.GetLength( 1 ) == 0 )
			return;
		for ( int x = 0; x < pathGrid.cells.GetLength( 0 ); x++ )
		{
			for ( int y = 0; y < pathGrid.cells.GetLength( 1 ); y++ )
			{
				PathGrid.Cell c = pathGrid.cells[x, y];
				Gizmos.color = c.traversable ? traverseColor : noTraverseColor;
				Gizmos.DrawCube( c.globalPos, Walls.cellSize );
			}
		}
		Gizmos.color = Color.red;
		Vector2Int gridPos = ClampToGrid( PlayerBaseClass.current.playerCenter.position );
		Vector3 pos = HelperClass.V2toV3( pathGrid.cells[gridPos.x, gridPos.y].globalPos );
		Gizmos.DrawSphere( pos, 1f / 2f );
	}

	public void regeneratePathGrid()
	{
		// just to keep it under control
		System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
		timer.Start();
		wallMask = LayerMask.NameToLayer( "Walls" );
		Walls.CompressBounds();
		Vector3Int dimensions = Walls.cellBounds.size;

		PathGrid newPathGrid = new PathGrid( dimensions.x, dimensions.y );

		for ( int n = Walls.cellBounds.xMin; n < Walls.cellBounds.xMax; n++ )
		{
			int x = n - Walls.cellBounds.xMin;
			for ( int p = Walls.cellBounds.yMin; p < Walls.cellBounds.yMax; p++ )
			{
				int y = p - Walls.cellBounds.yMin;
				Vector3Int localPlace = ( new Vector3Int( n, p, (int) Walls.transform.position.y ) );
				Vector3 place = Walls.CellToWorld( localPlace ) + Vector3.one / 2f;


				PathGrid.Cell c = new PathGrid.Cell();
				newPathGrid.cells[x, y] = c;
				c.gridPos = new Vector2Int( x, y );
				c.globalPos = place;

				//is it traversable?
				c.traversable = !Walls.HasTile( localPlace );
			}
		}

		for ( int x = 0; x < dimensions.x; x++ )
		{
			for ( int y = 0; y < dimensions.y; y++ )
			{

			}
		}
		timer.Stop();
		if ( curr.debugPaths )
			Debug.Log( string.Format(
			"Regenerated Pathing Grid\n" +
			"Took {0} ms\n" +
			"Grid size is now {1} by {2} cells ({3} cells total)",
			timer.Elapsed.TotalMilliseconds, newPathGrid.width, newPathGrid.height, newPathGrid.width * newPathGrid.height ) );

		pathGrid = newPathGrid;
	}

	[System.Serializable]
	public class PathGrid
	{
		[SerializeField]
		public Cell[,] cells;
		[SerializeField]
		public int width, height;

		public PathGrid( int width, int height )
		{
			this.width = width;
			this.height = height;

			cells = new Cell[width, height];
		}

		[System.Serializable]
		public class Cell
		{
			[SerializeField]
			public Vector2Int gridPos;
			[SerializeField]
			public Vector2 globalPos;
			[SerializeField]
			public bool traversable;

			public int HeapIndex
			{
				get;
				set;
			}

			public Cell( Vector2Int gridPos, Vector2 globalPos, bool traversable )
			{
				this.gridPos = gridPos;
				this.globalPos = globalPos;
				this.traversable = traversable;
			}

			public Cell()
			{

			}

			public int CompareTo( Cell other )
			{
				throw new NotImplementedException();
			}
		}
	}

	public class PathContext
	{
		[SerializeField]
		public PathGrid.Cell[,] cells;
		[SerializeField]
		public int width, height;
		public Heap<ActiveCell> Open;
		public List<ActiveCell> Closed;
		public Vector2Int target;

		public static List<Vector2Int> FindPath( Vector2Int start, Vector2Int end )
		{
			List<Vector2Int> path = new List<Vector2Int>();

			PathContext context = new PathContext( curr.pathGrid, end );
			ActiveCell c = new ActiveCell( curr.pathGrid.cells[start.x, start.y] );

			c.gCost = 0;
			c.hCost = context.Distance( c.gridPos, end );
			context.Open.Add( c );

			while ( context.Open.Count != 0 )
			{
				// find lowest f-cost
				ActiveCell current = context.Open.RemoveFirst();
				context.Closed.Add( current );
				if ( current.gridPos == end )
				{
					if ( curr.debugPaths )
					{
						Debug.Log( string.Format( "Path from {0},{1} to {2},{3} complete", start.x, start.y, end.x, end.y ) );
						Debug.Log( "Path finished after " + context.Closed.Count + " finished cells and " + context.Open.Count + " started cells" );
					}
					//thats the path
					current.PathTrain( ref context.Closed, ref path );
					return path;
				}

				context.GetNeighbors( current );
			}
			//throw new Exception( "Impossible Path" );
			return new List<Vector2Int>() { ClampToGrid( PlayerBaseClass.current.transform.position ) };
		}

		private void GetNeighbors( ActiveCell from )
		{
			Vector2Int gridPos = from.gridPos;
			List<PathGrid.Cell> neighbors = new List<PathGrid.Cell>();
			for ( int x = -1; x <= 1; x++ )
			{
				for ( int y = -1; y <= 1; y++ )
				{
					if ( x == 0 && y == 0 )
						continue;
					int tryX = gridPos.x + x;
					int tryY = gridPos.y + y;
					if ( tryX > 0 && tryX < cells.GetLength( 0 ) && tryY > 0 && tryY < cells.GetLength( 1 ) )
					{
						//the cell exists
						if ( cells[from.gridPos.x, tryY].traversable && cells[tryX, from.gridPos.y].traversable )
						{
							neighbors.Add( cells[tryX, tryY] );
						}
					}
				}
			}

			List<ActiveCell> fin = new List<ActiveCell>();

			foreach ( PathGrid.Cell c in neighbors )
			{
				bool found = false;
				foreach ( ActiveCell act in Closed )
				{
					if ( c.gridPos == act.gridPos )
					{
						found = true;
						break;
					}
				}
				if ( found )
				{
					continue;
				}
				foreach ( ActiveCell act in Open.items )
				{
					if ( act == null )
					{
						break;
					}
					if ( c.gridPos == act.gridPos )
					{
						fin.Add( act );
						found = true;
						break;
					}
				}

				if ( found )
				{
					continue;
				}

				//we havent made it yet
				//is it valid?
				if ( !c.traversable )
				{
					continue;
				}
				//its valid, lets make it!

				ActiveCell a = new ActiveCell( c );
				a.hCost = Distance( a.gridPos, target );
				a.gCost = uint.MaxValue;
				fin.Add( a );
			}
			foreach ( ActiveCell a in fin )
			{
				Vector2Int diff = from.gridPos - a.gridPos;
				bool diagonal = ( diff.x * diff.x + diff.y * diff.y ) == 2;
				uint newCost = from.gCost + (uint) ( diagonal ? 14 : 10 );
				if ( a.gCost > newCost )
				{
					a.bestFrom = gridPos;
					a.gCost = newCost;
				}
				if ( !Open.Contains( a ) )
				{
					Open.Add( a );
				}
			}
			return;
		}

		private uint Distance( Vector2Int from, Vector2Int to )
		{
			Vector2Int diff = from - to;
			diff.x = Math.Abs( diff.x );
			diff.y = Math.Abs( diff.y );
			bool xLowest = diff.x < diff.y;
			if ( xLowest )
			{
				return (uint) ( diff.x * 14 + ( diff.y - diff.x ) * 10 );
			}
			else
			{
				return (uint) ( diff.y * 14 + ( diff.x - diff.y ) * 10 );
			}
		}

		public PathContext( PathGrid parent, Vector2Int target )
		{
			width = parent.width;
			height = parent.height;
			cells = parent.cells;
			this.target = target;

			Open = new Heap<ActiveCell>( width * height );
			Closed = new List<ActiveCell>();
		}
		[System.Serializable]
		public class ActiveCell: PathGrid.Cell, IHeapItem<ActiveCell>
		{
			public uint gCost, hCost = 0;
			public uint fCost
			{
				get
				{
					return gCost + hCost;
				}
			}
			public Vector2Int bestFrom;

			public ActiveCell( PathGrid.Cell parent ) : base( parent.gridPos, parent.globalPos, parent.traversable )
			{

			}

			public ActiveCell() : base()
			{

			}
			public ActiveCell( Vector2Int gridPos, Vector2 globalPos, bool traversable ) : base( gridPos, globalPos, traversable )
			{

			}

			internal void PathTrain( ref List<ActiveCell> cells, ref List<Vector2Int> path )
			{
				if ( bestFrom == gridPos )
				{
					return;
				}
				foreach ( ActiveCell cell in cells )
				{
					if ( cell.gridPos == bestFrom )
					{
						cell.PathTrain( ref cells, ref path );
						path.Add( gridPos );
						return;
					}
				}
			}

			public int CompareTo( ActiveCell compareTo )
			{
				int compare = fCost.CompareTo( compareTo.fCost );
				if ( compare == 0 )
				{
					compare = hCost.CompareTo( compareTo.hCost );
				}
				return -compare;
			}
			/*public int CompareTo( Node nodeToCompare )
			{
				int compare = fCost.CompareTo( nodeToCompare.fCost );
				if ( compare == 0 )
				{
					compare = hCost.CompareTo( nodeToCompare.hCost );
				}
				return -compare;
			}*/
		}
	}
	#endregion

}

