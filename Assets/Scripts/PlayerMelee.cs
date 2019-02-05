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

	void Start()
	{
		player = PlayerBaseClass.current;
	}

	void Update()
	{
		if ( ( Input.GetButtonDown( "Fire1" ) || Input.GetKeyDown( KeyCode.LeftControl ) ) && player.playerMovement.movementState != PlayerMovement.PlayerMovementState.Fixed)// && !isMeleeing )
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
			meleeTransform.localPosition = defaultPos;
			if ( lastSwipe == MeleeSwipe.LeftToRight )
			{
				meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, rotMax );
			}
			else
			{
				meleeTransform.localEulerAngles = new Vector3( meleeTransform.localEulerAngles.x, meleeTransform.localEulerAngles.y, rotMin );
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
		}
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
		meleeTransform.localEulerAngles = new Vector3( meleeTransform.rotation.x, meleeTransform.rotation.x, totalRot );
	}
}


public enum MeleeSwipe
{
	LeftToRight,
	RightToLeft
}