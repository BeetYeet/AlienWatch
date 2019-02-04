using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController: MonoBehaviour
{

	public static GameController curr {
		get;
		private set;
	}
	public event System.Action Tick;
	public int ticksPerMinute = 70;
	private float TickTime
	{
		get
		{
			return 60f/ticksPerMinute;
		}
	}
	private float nextTick;

	void Awake() // Awake() går innan Start()
	{
		if ( curr != null )
			throw new System.Exception( "Too many instances of GameController, should only be one" );
		curr = this;
	}

	private void DoTick()
	{
        Tick?.Invoke();
		nextTick += TickTime;
	}

	void Update()
	{
		if ( Time.time > nextTick )
			DoTick();
	}
}
