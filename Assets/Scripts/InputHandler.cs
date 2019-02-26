using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler: MonoBehaviour
{

	public static InputHandler current;

	public List<AxisHandler> axies;

	void Awake()
	{
		if ( current != null )
		{
			throw new System.Exception( "Too many InputHandlers! (can only be one)" );
		}
		current = this;
		axies = new List<AxisHandler>();
		axies.Add( new AxisHandler( "Melee", .2f, false ) );
		axies.Add( new AxisHandler( "Dash", .2f, false ) );
		axies.Add( new AxisHandler( "Grenade", .2f, false ) );

	}

	public AxisHandler GetWithName( string name )
	{
		AxisHandler axis = null;

		axies.ForEach( ( x ) => { if ( x.name() == name ) { axis = x; } } );

		if ( axis == null )
		{
			throw new System.NullReferenceException();
		}
		return axis;
	}

	private void Update()
	{
		axies.ForEach( ( x ) => { x.Update(); } );
	}
}
public class AxisHandler
{
	bool wasActive = false;
	bool isActive = false;
	string axisName;
	float minValue = .2f;
	bool invert = false;

	public string name()
	{
		return axisName;
	}

	public bool GetButton()
	{
		return isActive;
	}

	public bool GetButtonUp()
	{
		return wasActive && !isActive;
	}

	public bool GetButtonDown()
	{
		return !wasActive && isActive;
	}

	public void Update() //not the monobehavior update
	{
		wasActive = isActive;
		isActive = Input.GetAxisRaw( axisName ) > minValue ^ invert;
	}

	public AxisHandler( string axisName, float minValue, bool invert )
	{
		this.axisName = axisName;
		this.minValue = minValue;
		this.invert = invert;
	}
}