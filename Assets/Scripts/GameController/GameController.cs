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

	public List<Vector2> PathfindAstar( Vector2 start, Vector2 end )
	{
		return null;
	}

	public Vector2Int ClampToGrid( Vector3 pos )
	{
		Vector3Int _ = Walls.WorldToCell( pos ) - new Vector3Int( Walls.cellBounds.xMin, Walls.cellBounds.yMin, 0 );
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
		Vector2Int gridPos = ClampToGrid( PlayerBaseClass.current.transform.position );
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
		Vector3Int dimensions = Walls.editorPreviewSize;

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

			public Cell( Vector2Int gridPos, Vector2 globalPos, bool traversable )
			{
				this.gridPos = gridPos;
				this.globalPos = globalPos;
				this.traversable = traversable;
			}

			public Cell()
			{

			}
		}
	}

	public class PathContext
	{
		[SerializeField]
		public PathGrid.Cell[,] cells;
		[SerializeField]
		public int width, height;
		public List<ActiveCell> Open;
		public List<ActiveCell> Closed;
		public Vector2Int target;

		public static List<Vector2Int> FindPath( Vector2Int start, Vector2Int end )
		{
			List<Vector2Int> path = new List<Vector2Int>();

			PathContext context = new PathContext( curr.pathGrid, end );
			ActiveCell c = new ActiveCell( curr.pathGrid.cells[start.x, start.y] );

			c.gCost = 0;
			c.hCost = context.GetHCost( c.gridPos );
			context.Open.Add( c );

			while ( context.Open.Count != 0 )
			{
				// find lowest f-cost
				ActiveCell current = context.Open[0];
				for ( int i = 1; i < context.Open.Count - 1; i++ )
				{
					if ( context.Open[i].fCost < current.fCost )
					{
						current = context.Open[i];
					}
				}
				if ( current.gridPos == end )
				{
					//thats the path
					current.PathTrain( ref context.Closed, ref path );
					return path;
				}

				context.GetNeighbors( current );

			}
			throw new Exception( "Impossible Path" );
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
						neighbors.Add( cells[tryX, tryY] );
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
						continue;
					}
				}
				if ( found )
				{
					continue;
				}
				{
					foreach ( ActiveCell act in Open )
					{
						if ( c.gridPos == act.gridPos )
						{
							fin.Add( act );
							found = true;
							continue;
						}
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
				a.hCost = GetHCost( a.gridPos );
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

		private uint GetHCost( Vector2Int enheritedPos )
		{
			throw new NotImplementedException();
		}


		public bool Next()
		{
			return false;
		}

		public PathContext( PathGrid parent, Vector2Int target )
		{
			width = parent.width;
			height = parent.height;
			cells = parent.cells;
			this.target = target;

			Open = new List<ActiveCell>();
			Closed = new List<ActiveCell>();
		}
		[System.Serializable]
		public class ActiveCell: PathGrid.Cell
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
		}
	}
	#endregion

}

