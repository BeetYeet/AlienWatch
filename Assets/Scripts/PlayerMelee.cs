using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee: MonoBehaviour
{
	PlayerBaseClass player;
	public float meleeTime = .2f;
	public float meleeTimeLeft = 0f;
	public bool isMeleeing
	{
		get
		{
			return meleeTimeLeft != 0f;
		}
	}
	public MeleeSwipe lastSwipe
	{
		get;
		private set;
	}
	public Collider2D meleeCollider;
	public Transform meleeTransform;
	public SpriteRenderer meleeSprite;
	public float meleeAnimPart
	{
		get
		{
			if ( !isMeleeing )
				return -1f;
			return 1f - meleeTimeLeft / meleeTime;
		}
	}
	public float rotMin = -60f;
	public float rotMax = 60f;
	public Vector3 defaultPos;
	public Vector3 forwardPos;
	public Vector3 swipeLeftPos;
	public Vector3 swipeRightPos;
	private PlayerMovement.PlayerDirection lastValidDirection = PlayerMovement.PlayerDirection.Forward;
	private int directionalRotation = 0;

	void Start()
	{
		player = PlayerBaseClass.current;
		GameController.curr.Tick += Tick;
	}

	void Tick()
	{
		directionalRotation = Mathf.RoundToInt( directionalRotation * .95f - GetRawRotation() * .05f );
		directionalRotation = NormalizeRotation( directionalRotation );
	}

	void Update()
	{
		if ( player.playerMovement.playerDir != PlayerMovement.PlayerDirection.None )
		{
			lastValidDirection = player.playerMovement.playerDir;
		}

		int validRotation = 0;


		if ( ( Input.GetButtonDown( "Fire1" ) || Input.GetKeyDown( KeyCode.LeftControl ) ) && player.playerMovement.movementState != PlayerMovement.PlayerMovementState.Fixed )// && !isMeleeing )
		{
			Debug.Log( "Did Melee" );
			meleeTimeLeft += meleeTime;
			player.playerMovement.TriggerFixed( meleeTime );
			lastSwipe = InvertSwipe( lastSwipe );
		}
		if ( meleeTimeLeft > 0f )
		{
			meleeTimeLeft -= Time.deltaTime;
			if ( meleeTimeLeft < 0 )
			{
				meleeTimeLeft = 0f;
				meleeSprite.flipX = !meleeSprite.flipX;
			}
			else if ( meleeTimeLeft > 0f )
			{
				DoMeleeDamage();
			}

		}
		if ( !isMeleeing )
		{
			meleeTransform.localPosition = HelperClass.RotateAroundAxis( new Vector2( defaultPos.x, defaultPos.y ), Vector2.zero, GetRotationFromDirection() );
			if ( lastSwipe == MeleeSwipe.LeftToRight )
			{
				meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, rotMax - GetRotationFromDirection() );
			}
			else
			{
				meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, rotMin - GetRotationFromDirection() );
			}
		}
		else
		{
			if ( lastSwipe == MeleeSwipe.LeftToRight )
			{
				float _ = meleeAnimPart;
				if ( _ < .2f )
				{
					_ *= 5f;
					meleeTransform.localPosition = Vector3.Lerp( defaultPos, swipeRightPos, _ );
				}
				else if ( _ >= .2f && _ < .6 )
				{
					_ = ( _ * 5 - 1f ) / 2f;
					meleeTransform.localPosition = Vector3.Lerp( swipeRightPos, forwardPos, _ );
				}
				else
				{
					_ = ( _ * 5 - 3f ) / 2f;
					meleeTransform.localPosition = Vector3.Lerp( forwardPos, defaultPos, _ );
				}
			}
			else
			{
				float _ = meleeAnimPart;
				if ( _ < .2f )
				{
					_ *= 5f;
					meleeTransform.localPosition = Vector3.Lerp( defaultPos, swipeLeftPos, _ );
				}
				else if ( _ >= .2f && _ < .6 )
				{
					_ = ( _ * 5 - 1f ) / 2f;
					meleeTransform.localPosition = Vector3.Lerp( swipeLeftPos, forwardPos, _ );
				}
				else
				{
					_ = ( _ * 5 - 3f ) / 2f;
					meleeTransform.localPosition = Vector3.Lerp( forwardPos, defaultPos, _ );
				}
			}
			meleeTransform.position = HelperClass.RotateAroundAxis( new Vector2( meleeTransform.position.x, meleeTransform.position.y ), transform.position, GetRotationFromDirection() );
		}

		validRotation = NormalizeRotation( validRotation - directionalRotation );
		meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, validRotation );
	}

	private int NormalizeRotation( int rot )
	{
		return ( ( rot + 180 ) % 360 ) - 180;
	}

	public MeleeSwipe InvertSwipe( MeleeSwipe swipe )
	{
		if ( swipe == MeleeSwipe.LeftToRight )
		{
			return MeleeSwipe.RightToLeft;
		}
		return MeleeSwipe.LeftToRight;
	}

	private void DoMeleeDamage()
	{
		//TODO: melee damage
		float totalRot = 0f;

		if ( lastSwipe == MeleeSwipe.LeftToRight )
		{
			totalRot = rotMin + ( rotMax - rotMin ) * meleeAnimPart;
			Debug.Log( "Swiping Right" );
		}
		else
		{
			totalRot = rotMax - ( rotMax - rotMin ) * meleeAnimPart;
			Debug.Log( "Swiping Left" );
		}

		//add rotation depending on direction
		totalRot -= GetRotationFromDirection();

		meleeTransform.localEulerAngles = new Vector3( meleeTransform.rotation.x, meleeTransform.rotation.x, totalRot );
	}
	public float GetRotationFromDirection()
	{
		return directionalRotation;
	}

	public float GetRawRotation()
	{
		switch ( lastValidDirection )
		{
			case PlayerMovement.PlayerDirection.ForwardRight:
				return 45f;
			case PlayerMovement.PlayerDirection.Right:
				return 90f;
			case PlayerMovement.PlayerDirection.BackwardRight:
				return 135f;
			case PlayerMovement.PlayerDirection.Backward:
				return 180f;
			case PlayerMovement.PlayerDirection.BackwardLeft:
				return 225f;
			case PlayerMovement.PlayerDirection.Left:
				return 270f;
			case PlayerMovement.PlayerDirection.ForwardLeft:
				return 315f;
			default:
				return 0f;
		}
	}
}


public enum MeleeSwipe
{
	LeftToRight,
	RightToLeft
}