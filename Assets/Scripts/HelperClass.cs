using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass
{
	public static Vector2 RotateAroundAxis( Vector2 point, Vector2 rotationAxis, float degrees )
	{
		degrees *= Mathf.PI / 180f;
		float x, y = 0f;
		point -= rotationAxis;
		x = point.x * Mathf.Cos( degrees ) - point.y * Mathf.Sin( degrees );
		y = point.x * Mathf.Sin( degrees ) + point.y * Mathf.Cos( degrees );
		return new Vector2( x, y ) + rotationAxis;
	}
	public static Vector2 V3toV2( Vector3 v )
	{
		return new Vector2( v.x, v.y );
	}

	public static Vector3 V2toV3( Vector2 v )
	{
		return new Vector3( v.x, v.y, 0f );
	}
}
