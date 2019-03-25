using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	private void OnValidate()
	{
		ticksPerMinute = 480;
	}
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
}
