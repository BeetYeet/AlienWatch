using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
	PlayerBaseClass player;
	[SerializeField]
	public PlayerDirection playerDir; // which direction is the player moving, or none?
	[SerializeField]
	public PlayerMovementState movementState; // what is the player doing now movement-wise
	[SerializeField]
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
		HandleDashing();
		HandleMovement();
	}

	private void HandleDashing()
	{
		if ( !canMove )
		{
			return;
		}
		if ( Input.GetKeyDown( KeyCode.Space ) )
		{
			Debug.Log( "wants to dash" );
			playerDir = GetDirection();
			if ( true && playerDir != PlayerDirection.None )// ( player.ManaScript.manaValue >= manaForDash )
			{
				//player.ManaScript.manaValue -= manaForDash;
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
			playerDir = GetDirection();
			MovePlayer();
		}
	}

	private void MovePlayer()
	{
		Vector2 input = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
		Debug.Log( input.x + ",\t" + input.y );
		if ( input == Vector2.zero )
		{
			movementState = PlayerMovementState.Still;
			player.rigidbody.velocity = Vector2.zero;
			return;
		}
		Vector2 dir = input.normalized;
		Debug.Log( dir.x + ",\t" + dir.y );
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
				return new Vector2(1f, 1f).normalized;
			case PlayerDirection.Right:
				return Vector2.right;
			case PlayerDirection.BackwardRight:
				return new Vector2( 1f, -1f ).normalized;
			case PlayerDirection.Backward:
				return Vector2.up*-1f;
			case PlayerDirection.BackwardLeft:
				return new Vector2( -1f, -1f ).normalized;
			case PlayerDirection.Left:
				return Vector2.right*-1f;
			case PlayerDirection.ForwardLeft:
				return new Vector2( -1f, 1f ).normalized;
		}
	}

	private PlayerDirection GetDirection()
	{
		Vector2 stick = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
		for ( int i = 0; i < 1; i++ )
		{ //make a loop to run a single time so we can use 'break'
			if ( stick.SqrMagnitude() <= .175f ) // is it in the middle-ish
			{
				break;
			}
			stick.Normalize();
			float root3over2 = Mathf.Sqrt( 3 ) / 2;
			if ( stick.x > root3over2			&&	 Mathf.Abs( stick.y ) < 1 / 2 )
			{
				return  PlayerDirection.Left;
			}
			if ( stick.x < -root3over2			&&	 Mathf.Abs( stick.y ) < 1 / 2 )
			{
				return  PlayerDirection.Right;
			}
			if ( Mathf.Abs( stick.x ) < 1 / 2	&&	 stick.y > root3over2 )
			{
				return  PlayerDirection.Forward;
			}
			if ( Mathf.Abs( stick.x ) < 1 / 2	&&	 stick.y < -root3over2 )
			{
				return  PlayerDirection.Backward;
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
				return  PlayerDirection.ForwardRight;
			}
			if ( !isRight && isForward )
			{
				return  PlayerDirection.ForwardLeft;
			}
			if ( isRight && !isForward )
			{
				return  PlayerDirection.BackwardRight;
			}
			if ( !isRight && !isForward )
			{
				return  PlayerDirection.BackwardLeft;
			}
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
