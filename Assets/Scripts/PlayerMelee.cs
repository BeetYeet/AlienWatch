﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee: MonoBehaviour
{
	public float pausePercent = .5f;
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
	private float directionalRotation = 0;
	public float rotationAgressiveness = .1f;
	public SwordReturn returnTo = SwordReturn.DontReturn;
	public bool flip;

	void Start()
	{
		player = PlayerBaseClass.current;
		GameController.curr.Tick += Tick;
	}

	void Tick()
	{
	}

	void Update()
	{

		float shortest_angle = ( ( ( ( GetRawRotation() - directionalRotation ) % 360 ) + 540 ) % 360 ) - 180;

		directionalRotation = NormalizeRotation( directionalRotation + shortest_angle * Time.deltaTime * rotationAgressiveness );
		float validRotation = 0;

		if ( InputHandler.current.GetWithName( "Melee" ).GetButtonDown() && player.playerMovement.movementState != PlayerMovement.PlayerMovementState.Fixed )// && !isMeleeing )
		{
			TriggerSwipe();
		}
		else
		{
			switch ( returnTo )
			{
				default:
					break;
				case SwordReturn.ReturnLeft:
					if ( !isMeleeing && lastSwipe == MeleeSwipe.LeftToRight && player.playerMovement.movementState != PlayerMovement.PlayerMovementState.Fixed )
					{
						TriggerSwipe();
					}
					break;
				case SwordReturn.ReturnRight:
					if ( !isMeleeing && lastSwipe == MeleeSwipe.RightToLeft && player.playerMovement.movementState != PlayerMovement.PlayerMovementState.Fixed )
					{
						TriggerSwipe();
					}
					break;
			}
		}
		if ( meleeTimeLeft > 0f )
		{
			meleeTimeLeft -= Time.deltaTime;
			if ( meleeTimeLeft < 0 )
			{
				meleeTimeLeft = 0f;
				meleeSprite.flipX = lastSwipe == MeleeSwipe.LeftToRight ^ flip;
			}
			else if ( meleeTimeLeft > 0f )
			{
				DoMeleeDamage();

				float totalRot = 0f;
				if ( lastSwipe == MeleeSwipe.LeftToRight )
				{
					totalRot = rotMin + ( rotMax - rotMin ) * meleeAnimPart;
				}
				else
				{
					totalRot = rotMax - ( rotMax - rotMin ) * meleeAnimPart;
				}

				validRotation += totalRot;
			}

		}
		if ( !isMeleeing )
		{
			meleeTransform.localPosition = HelperClass.RotateAroundAxis( new Vector2( defaultPos.x, defaultPos.y ), Vector2.zero, GetRotationFromDirection() );
			if ( lastSwipe == MeleeSwipe.LeftToRight )
			{
				validRotation += rotMax;
			}
			else
			{
				validRotation += rotMin;
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

		meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, NormalizeRotation( validRotation + directionalRotation ) );
		Vector2 _1 = HelperClass.RotateAroundAxis( Vector2.up, Vector2.zero, NormalizeRotation( validRotation + directionalRotation ) );
		Vector2 _2 = HelperClass.RotateAroundAxis( Vector2.up, Vector2.zero, directionalRotation );
		Vector2 _3 = HelperClass.RotateAroundAxis( Vector2.up, Vector2.zero, GetRawRotation() );
		Debug.DrawLine(
		transform.position,
		transform.position + new Vector3( _1.x, _1.y ),
				Color.green
				);
		Debug.DrawLine(
		transform.position,
		transform.position + new Vector3( _2.x, _2.y ),
		Color.cyan
		);
		Debug.DrawLine(
		transform.position,
		transform.position + new Vector3( _3.x, _3.y ),
		Color.red
		);
	}

	private void TriggerSwipe()
	{
		meleeTimeLeft += meleeTime - meleeTimeLeft;
		player.playerMovement.TriggerFixed( meleeTimeLeft * pausePercent );
		lastSwipe = InvertSwipe( lastSwipe );
	}

	private float NormalizeRotation( float rot )
	{
		return ( ( rot + 180f ) % 360f ) - 180f;
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
	}
	public float GetRotationFromDirection()
	{
		return directionalRotation;
	}

	public float GetRawRotation()
	{
		switch ( PlayerBaseClass.current.playerMovement.lastValidDirection )
		{
			case PlayerMovement.PlayerDirection.ForwardRight:
				return 315f;
			case PlayerMovement.PlayerDirection.Right:
				return 270f;
			case PlayerMovement.PlayerDirection.BackwardRight:
				return 225f;
			case PlayerMovement.PlayerDirection.Backward:
				return 180f;
			case PlayerMovement.PlayerDirection.BackwardLeft:
				return 135f;
			case PlayerMovement.PlayerDirection.Left:
				return 90f;
			case PlayerMovement.PlayerDirection.ForwardLeft:
				return 45f;
			default:
				return 0f;
		}
	}
}

public enum SwordReturn
{
	DontReturn,
	ReturnLeft,
	ReturnRight
}

public enum MeleeSwipe
{
	LeftToRight,
	RightToLeft
}