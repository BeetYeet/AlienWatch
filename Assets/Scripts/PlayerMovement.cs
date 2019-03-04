using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
	PlayerBaseClass player;
	[SerializeField]
	public PlayerDirection playerDir; // which direction is the player moving, or none?
	public PlayerDirection lastValidDirection = PlayerDirection.Forward;
	[SerializeField]
	public PlayerMovementState movementState; // what is the player doing now movement-wise
	bool canMove // can the player move or dash?
	{
		get
		{
			return fixedTimeLeft == 0f;
		}
	}
	[SerializeField]
	float fixedTimeLeft = 0f; // how long untill the player can move (seconds)
	public float movementSpeed; // how fast does the player move (U/s)
	[SerializeField]
	float dashTimeLeft = 0f; // how long untill the player stops dashing
	public float dashTime = 5f; // how long is a dash (seconds)
	public float dashSpeed = .5f; // how fast is a dash (U/s)
	public int manaForDash = 20; // how much mana does it take to dash
	[SerializeField]
	public PlayerDirection dashDirection; // which direction did the player dash

	void Start()
	{
		player = PlayerBaseClass.current;
	}

	// Update is called once per frame
	void Update()
	{
		playerDir = GetDirection();
		HandleDashing();
		HandleMovement();
		if ( playerDir != PlayerDirection.None )
		{
			lastValidDirection = playerDir;
		}
	}

	private void HandleDashing()
	{
		if ( !canMove )
		{
			return;
		}
		if ( movementState != PlayerMovementState.Dashing && InputHandler.current.GetWithName( "Dash" ).GetButtonDown() )
		{
			if ( player.playerMana.mana >= manaForDash && playerDir != PlayerDirection.None )
			{
				player.playerMana.mana -= manaForDash;
				dashTimeLeft += dashTime;
				dashDirection = playerDir;
			}
		}
	}

	private void HandleMovement()
	{
		if ( dashTimeLeft > 0f )
		{
			dashTimeLeft -= Time.deltaTime;
			if ( dashTimeLeft > 0f )
			{
				movementState = PlayerMovementState.Dashing;
				player.rigidbody.velocity = dashSpeed * GetVectorDirection( dashDirection );
				//TODO: Dash damage
				return;
			}
			else
			{
				dashTimeLeft = 0f;
				movementState = PlayerMovementState.Still;
			}
		}
		if ( fixedTimeLeft > 0f )
		{
			fixedTimeLeft -= Time.deltaTime;
			if ( fixedTimeLeft > 0 )
			{
				movementState = PlayerMovementState.Fixed;
				player.rigidbody.velocity = Vector2.zero;
			}
			else
			{
				if ( fixedTimeLeft < 0f )
				{
					fixedTimeLeft = 0f;
					movementState = PlayerMovementState.Still;
				}
			}

		}
		if ( canMove )
		{
			MovePlayer();
		}
	}

	private void MovePlayer()
	{
		Vector2 input = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
		if ( input == Vector2.zero )
		{
			movementState = PlayerMovementState.Still;
			player.rigidbody.velocity = Vector2.zero;
			return;
		}
		Vector2 dir = input.normalized;
		dir = new Vector2( Mathf.Abs( dir.x ), Mathf.Abs( dir.y ) );
		// account for the distance in a circle so that moving to the topleft is the same speed as to the left by
		//		multiplying by a normalized version
		player.rigidbody.velocity = input * dir * movementSpeed;
		movementState = PlayerMovementState.Moving;
	}

	public void TriggerFixed( float duration )
	{
		if ( movementState == PlayerMovementState.Dashing )
		{
			// player is dashing... how was he hit?
			Debug.LogWarning( "Player is dashing, how was he hit?" );
		}
		else
		{
			//he wasn't dashing, fix him
			fixedTimeLeft += duration;
			if ( !canMove )
			{
				movementState = PlayerMovementState.Fixed;
			}
		}

	}

	public static Vector2 GetVectorDirection( PlayerDirection dir )
	{
		switch ( dir )
		{
			default: // for None, since switches handle enums poorly with return statements
				return Vector2.zero;
			case PlayerDirection.Forward:
				return Vector2.up;
			case PlayerDirection.ForwardRight:
				return new Vector2( 1f, 1f ).normalized;
			case PlayerDirection.Right:
				return Vector2.right;
			case PlayerDirection.BackwardRight:
				return new Vector2( 1f, -1f ).normalized;
			case PlayerDirection.Backward:
				return Vector2.up * -1f;
			case PlayerDirection.BackwardLeft:
				return new Vector2( -1f, -1f ).normalized;
			case PlayerDirection.Left:
				return Vector2.right * -1f;
			case PlayerDirection.ForwardLeft:
				return new Vector2( -1f, 1f ).normalized;
		}
	}

	private PlayerDirection GetDirection()
	{
		Vector2 stick = new Vector2( Input.GetAxisRaw( "Horizontal" ), Input.GetAxisRaw( "Vertical" ) );
		if ( stick.SqrMagnitude() <= .175f ) // is it in the middle-ish
		{
			return PlayerDirection.None;
		}
		stick.Normalize();
		float root3over2 = Mathf.Sqrt( 3f ) / 2f;
		if ( stick.x > root3over2 && Mathf.Abs( stick.y ) < 1f / 2f )
		{
			return PlayerDirection.Right;
		}
		if ( stick.x < -root3over2 && Mathf.Abs( stick.y ) < 1f / 2f )
		{
			return PlayerDirection.Left;
		}
		if ( Mathf.Abs( stick.x ) < 1f / 2f && stick.y > root3over2 )
		{
			return PlayerDirection.Forward;
		}
		if ( Mathf.Abs( stick.x ) < 1f / 2f && stick.y < -root3over2 )
		{
			return PlayerDirection.Backward;
		}

		bool isRight = false, isForward = false;

		if ( stick.x > 0 )
		{
			// det är till höger
			isRight = true;
		}
		if ( stick.y > 0 )
		{
			// det är till höger
			isForward = true;
		}
		if ( isRight && isForward )
		{
			return PlayerDirection.ForwardRight;
		}
		if ( !isRight && isForward )
		{
			return PlayerDirection.ForwardLeft;
		}
		if ( isRight && !isForward )
		{
			return PlayerDirection.BackwardRight;
		}
		if ( !isRight && !isForward )
		{
			return PlayerDirection.BackwardLeft;
		}
		return PlayerDirection.None;
	}

	public enum PlayerDirection // for sprites
	{
		None,
		Forward,
		ForwardRight,
		Right,
		BackwardRight,
		Backward,
		BackwardLeft,
		Left,
		ForwardLeft
	}

	public enum PlayerMovementState // for keeping track of the status of the player
	{
		Still,
		Moving,
		Dashing,
		Fixed
	}
}
