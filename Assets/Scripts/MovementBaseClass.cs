using System;
using System.Collections.Generic;
using UnityEngine;

public class MovementBaseClass: MonoBehaviour
{
	public Vector2 knockBackVelocity = Vector2.zero;
	public Vector2 movementVelocity = Vector2.zero;
	public float knockBackDecay = 1.1f;
	public float knockbackWeight = 1f;
	public Rigidbody2D rb;
	public float velocityAgressiveness = .5f;

	private void FixedUpdate()
	{
		knockBackVelocity /= knockBackDecay;
		rb.velocity = Vector2.Lerp( rb.velocity, knockBackVelocity + movementVelocity, velocityAgressiveness );
	}

	public void DoKnockback( Vector2 vector )
	{
		knockBackVelocity += vector / knockbackWeight;
	}
}

