using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
	public PlayerDirection playerDir
	{
		get;
		private set;
	}

	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private PlayerDirection GetPLayerDirection()
	{
		PlayerDirection pd = PlayerDirection.None;
		Vector2 stick = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
		for ( int i = 0; i < 1; i++ )
		{ //make a loop to run a single time so we can use 'break'
			if ( stick.SqrMagnitude() >= .25f ) // is it in the middle-ish
			{
				break;
			}
			stick.Normalize();
			if ( stick.x > Mathf.Pow( 3, 1 / 2 ) / 2 && Mathf.Abs( stick.y ) < 1 / 2 )
			{
				pd = PlayerDirection.Left;
				break;
			}
			if ( stick.x < -Mathf.Pow( 3, 1 / 2 ) / 2 && Mathf.Abs( stick.y ) < 1 / 2 )
			{
				pd = PlayerDirection.Right;
				break;
			}
			if ( stick.y > Mathf.Pow( 3, 1 / 2 ) / 2 && Mathf.Abs( stick.x ) < 1 / 2 )
			{
				pd = PlayerDirection.Forward;
				break;
			}
			if ( stick.y < -Mathf.Pow( 3, 1 / 2 ) / 2 && Mathf.Abs( stick.x ) < 1 / 2 )
			{
				pd = PlayerDirection.Backward;
				break;
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
				pd = PlayerDirection.ForwardRight;
				break;
			}
			if ( !isRight && isForward )
			{
				pd = PlayerDirection.ForwardLeft;
				break;
			}
			if ( isRight && !isForward )
			{
				pd = PlayerDirection.BackwardRight;
				break;
			}
			if ( !isRight && !isForward )
			{
				pd = PlayerDirection.BackwardLeft;
				break;
			}
		}
		return pd;
	}


	public enum PlayerDirection
	{
		None,
		Forward,
		Backward,
		Left,
		Right,
		ForwardLeft,
		ForwardRight,
		BackwardLeft,
		BackwardRight
	}

}
