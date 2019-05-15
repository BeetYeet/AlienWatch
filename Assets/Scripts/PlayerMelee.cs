using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee: MonoBehaviour
{
	#region vars
	public float pausePercent = .5f;
	PlayerBaseClass player;
	public int damage;
	public float preMeleeCooldown = .1f;
	public float postMeleeCooldown = .1f;
	float currPreMeleeCooldown = 0f;
	float currPostMeleeCooldown = 0f;

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

	public event Action SwingTrigger; // Player wants to swing
	public event Action SwingRecover; // The sword cooldown is over
	public event Action SwingStart; // The sword starts moving
	public event Action SwingEnd; // The sword stops moving

	public bool swordSheathed = true;
	public float sheatheTime = 2f;
	public float timeUntilSheathed;
	public event Action OnSheathe;
	public event Action OnUnsheathe;
	public bool debugSheathe = false;
	#endregion

	void Start()
	{
		player = PlayerBaseClass.current;
		GameController.curr.Tick += Tick;
		SwingTrigger += () =>
		{
			//Debug.Log( "SwingTrigger" ); 
		};
		SwingRecover += () =>
		{
			//Debug.Log( "SwingRecover" ); 
		};
		SwingStart += () =>
		{
			//Debug.Log( "SwingStart" ); 
		};
		SwingEnd += () =>
		{
			//Debug.Log( "SwingEnd" );
		};
		OnSheathe += () =>
		{
			if ( debugSheathe )
				Debug.Log( "Sheathed" );
			swordSheathed = true;
			meleeSprite.enabled = false;
		};
		OnUnsheathe += () =>
		{
			if ( debugSheathe )
				Debug.Log( "Unsheathed" );
			swordSheathed = false;
			meleeSprite.enabled = true;
		};
		OnSheathe();
	}

	void Tick()
	{

	}

	void Update()
	{

		if ( HandleSheathe() )
		{


			meleeSprite.flipX = lastSwipe == MeleeSwipe.LeftToRight ^ flip ^ isMeleeing;
			float shortest_angle = NormalizeRotation( GetRawRotation( PlayerBaseClass.current.playerMovement.lastValidDirection ), directionalRotation );

			directionalRotation = directionalRotation + shortest_angle * Time.deltaTime * rotationAgressiveness;
			float validRotation = 0;

			if ( currPostMeleeCooldown != 0f )
			{
				currPostMeleeCooldown -= Time.deltaTime;
				if ( currPostMeleeCooldown < 0f )
				{
					currPostMeleeCooldown = 0f;
					SwingRecover();
				}
			}
			if ( currPreMeleeCooldown != 0f )
			{
				currPreMeleeCooldown -= Time.deltaTime;
				if ( currPreMeleeCooldown < 0f )
				{
					currPreMeleeCooldown = 0f;
					StartSwipe();
				}
			}
			if ( InputHandler.current.GetWithName( "Melee" ).GetButtonDown() )// && !isMeleeing )
			{
				if ( preMeleeCooldown == 0f )
				{
					StartSwipe();
					SwingTrigger();
				}
				else
				{
					currPreMeleeCooldown += preMeleeCooldown;
					SwingTrigger();
				}
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

			meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, NormalizeRotation( directionalRotation, validRotation ) );
			DrawDebug( validRotation );
		}

	}
	private void DrawDebug( float validRotation )
	{
		Vector2 _1 = HelperClass.RotateAroundAxis( Vector2.up, Vector2.zero, NormalizeRotation( directionalRotation, validRotation ) );
		Vector2 _2 = HelperClass.RotateAroundAxis( Vector2.up, Vector2.zero, directionalRotation );
		Vector2 _3 = HelperClass.RotateAroundAxis( Vector2.up, Vector2.zero, GetRawRotation( PlayerBaseClass.current.playerMovement.lastValidDirection ) );
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

	private float NormalizeRotation( float a, float b )
	{
		return ( ( ( ( a - b ) % 360 ) + 540 ) % 360 ) - 180;
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
		SoundManager.PlaySound( "swordSwipe" );
		SwingStart();
	}

	private void EndSwipe()
	{
		meleeTimeLeft = 0f;
		currPostMeleeCooldown += postMeleeCooldown;
		ColliderHandler.EndSwing();
		SwingEnd();
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

	public static float GetRawRotation( PlayerMovement.PlayerDirection dir )
	{
		switch ( dir )
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


	private bool HandleSheathe()
	{
		if ( Input.GetButtonDown( "Melee" ) )
		{
			if ( timeUntilSheathed == 0f )
			{
				OnUnsheathe?.Invoke();
			}
			timeUntilSheathed = sheatheTime;
		}
		if ( timeUntilSheathed == 0f )
		{
			return true;
		}
		if ( timeUntilSheathed > 0 )
		{
			timeUntilSheathed -= Time.deltaTime;
			if ( timeUntilSheathed <= 0f )
			{
				timeUntilSheathed = 0f;
				OnSheathe?.Invoke();
				return false;
			}
			else
			{
				return true;
			}
		}
		return false;
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