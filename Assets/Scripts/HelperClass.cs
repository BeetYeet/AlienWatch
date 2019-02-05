using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperClass
{
	public static Vector2 RotateAroundAxis(Vector2 point, Vector2 rotationAxis, float degrees)
	{
		degrees *= 180 / Mathf.PI;
		float x, y = 0f;
		point -= rotationAxis;
		x = point.x * Mathf.Cos( degrees ) - point.y * Mathf.Sin( degrees );
		y = point.x * Mathf.Sin( degrees ) + point.y * Mathf.Cos( degrees );
		return new Vector2(x, y) + rotationAxis;
	}
}
