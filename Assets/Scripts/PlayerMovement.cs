using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MovementBaseClass
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

	public DashHandler dashHandler;

	public event Action DashStart;
	public event Action DashEnd;
	public event Action FixedStart;
	public event Action FixedEnd;

	public LayerMask notDash;
	public LayerMask dash;

	void Start()
	{

		player = PlayerBaseClass.current;
		rb = player.manualRigidBody;
		DashStart += () =>
		{
			dashHandler.active = true;
			//GetComponent<Collider2D>().enabled = false;
			gameObject.layer = LayerMask.NameToLayer( "PlayerDashing" );
		};
		DashEnd += () =>
		{
			dashHandler.active = false;
			//GetComponent<Collider2D>().enabled = true;
			gameObject.layer = LayerMask.NameToLayer( "Player" );
			;
		};
		FixedStart += () =>
		{
			//Debug.Log( "Get yooted" );
		};
		FixedEnd += () =>
		{
			//Debug.Log( "Get yooted" );
		};
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
				DashStart();
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
				knockBackVelocity = Vector2.zero;
				movementVelocity = dashSpeed * GetVectorDirection( dashDirection );
				//TODO: Dash damage
				return;
			}
			else
			{
				dashTimeLeft = 0f;
				movementState = PlayerMovementState.Still;
				DashEnd();
			}
		}
		if ( fixedTimeLeft > 0f )
		{
			fixedTimeLeft -= Time.deltaTime;
			if ( fixedTimeLeft > 0 )
			{
				movementState = PlayerMovementState.Fixed;
				player.manualRigidBody.velocity = Vector2.zero;
			}
			else
			{
				fixedTimeLeft = 0f;
				movementState = PlayerMovementState.Still;
				FixedEnd();
			}

		}
		if ( canMove )
		{
			MovePlayer();
		}
	}

	private void MovePlayer()
	{
		movementVelocity = Vector2.zero;
		Vector2 input = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
		if ( input == Vector2.zero )
		{
			movementState = PlayerMovementState.Still;
			return;
		}
		Vector2 dirFactor = input.normalized;
		dirFactor = new Vector2( Mathf.Abs( dirFactor.x ), Mathf.Abs( dirFactor.y ) );
		// account for the distance in a circle so that moving to the topleft is the same speed as to the left by
		//		multiplying by a normalized absolute version
		movementVelocity = input * dirFactor * movementSpeed;
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
			bool wasFixed = movementState == PlayerMovementState.Fixed;
			//he wasn't dashing, fix him
			fixedTimeLeft += duration;
			if ( !canMove )
			{
				movementState = PlayerMovementState.Fixed;
				if ( !wasFixed )
					FixedStart();
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
		//print( stick.SqrMagnitude() );
		if ( stick.SqrMagnitude() <= .005f ) // is it in the middle-ish
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
