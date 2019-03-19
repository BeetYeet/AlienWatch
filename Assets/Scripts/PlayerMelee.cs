using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee: MonoBehaviour
{
	public float pausePercent = .5f;
	PlayerBaseClass player;
	public int damage;
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
	public MeleeDamage ColliderHandler;
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
	public float strength = 5f;

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

		meleeSprite.flipX = lastSwipe == MeleeSwipe.LeftToRight ^ flip ^ isMeleeing;
		float shortest_angle = ( ( ( ( GetRawRotation() - directionalRotation ) % 360 ) + 540 ) % 360 ) - 180;

		directionalRotation = NormalizeRotation( directionalRotation + shortest_angle * Time.deltaTime * rotationAgressiveness );
		float validRotation = 0;

		if ( InputHandler.current.GetWithName( "Melee" ).GetButtonDown() )// && !isMeleeing )
		{
			StartSwipe();
		}
		else
		{
			switch ( returnTo )
			{
				default:
					break;
				case SwordReturn.ReturnLeft:
					if ( !isMeleeing && lastSwipe == MeleeSwipe.LeftToRight )
					{
						StartSwipe();
					}
					break;
				case SwordReturn.ReturnRight:
					if ( !isMeleeing && lastSwipe == MeleeSwipe.RightToLeft )
					{
						StartSwipe();
					}
					break;
			}
		}
		if ( meleeTimeLeft > 0f )
		{
			validRotation = GetValidRot( validRotation );
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
			DoPosition();
		}

		meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, NormalizeRotation( validRotation + directionalRotation ) );
		DrawDebug( validRotation );
	}

	private void DrawDebug( float validRotation )
	{
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

	private void DoPosition()
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

	private float GetValidRot( float validRotation )
	{
		meleeTimeLeft -= Time.deltaTime;
		if ( meleeTimeLeft < 0 )
		{
			EndSwipe();
		}
		else if ( meleeTimeLeft > 0f )
		{
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

		return validRotation;
	}

	private void StartSwipe()
	{
		meleeTimeLeft = meleeTime - meleeTimeLeft;
		player.playerMovement.TriggerFixed( meleeTimeLeft * pausePercent );
		lastSwipe = InvertSwipe( lastSwipe );
		ColliderHandler.StartSwing();
	}

	private void EndSwipe()
	{
		meleeTimeLeft = 0f;
		ColliderHandler.EndSwing();
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