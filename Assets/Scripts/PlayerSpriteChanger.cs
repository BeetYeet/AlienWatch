using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSpriteChanger : MonoBehaviour
{
	public float AnimationTime = 0.3f;
	public float CurrentWait = 0f;

	[Space]
	public Sprite Up;
	public List<Sprite> UpMove;
	[Space]
	public Sprite Down;
	public List<Sprite> DownMove;
	[Space]
	public Sprite Left;
	public List<Sprite> LeftMove;
	[Space]
	public Sprite Right;
	public List<Sprite> RightMove;

	private SpriteRenderer sprite;

	private void OnValidate()
	{
		sprite = GetComponent<SpriteRenderer>();

	}

	void Update()
	{
		if (PlayerBaseClass.current.playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
		{

			if (CurrentWait > 0)
			{
				CurrentWait -= Time.deltaTime;
			}
			if (CurrentWait <= 0f)
			{
				CurrentWait += AnimationTime;
				{
					if (PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.Forward ||
						PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.ForwardRight ||
						PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.ForwardLeft)
					{
						Sprite _ = UpMove[0];
						UpMove.RemoveAt(0);
						UpMove.Add(_);
						sprite.sprite = _;

						PlayerBaseClass.current.playerMelee.meleeSprite.transform.localPosition = 
							new Vector3(PlayerBaseClass.current.playerMelee.meleeSprite.transform.localPosition.x, PlayerBaseClass.current.playerMelee.meleeSprite.transform.localPosition.y, -11);
					}
					else
					{
						PlayerBaseClass.current.playerMelee.meleeSprite.transform.localPosition =
						new Vector3(PlayerBaseClass.current.playerMelee.meleeSprite.transform.localPosition.x, PlayerBaseClass.current.playerMelee.meleeSprite.transform.localPosition.y, -9);
					}
					if (PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.Backward ||
						PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.BackwardLeft ||
						PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.BackwardRight)
					{
						Sprite _ = DownMove[0];
						DownMove.RemoveAt(0);
						DownMove.Add(_);
						sprite.sprite = _;
					}if (PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.Left )
					{
						Sprite _ = LeftMove[0];
						LeftMove.RemoveAt(0);
						LeftMove.Add(_);
						sprite.sprite = _;
					}if (PlayerBaseClass.current.playerMovement.lastValidDirection ==
						PlayerMovement.PlayerDirection.Right)
					{
						Sprite _ = RightMove[0];
						RightMove.RemoveAt(0);
						RightMove.Add(_);
						sprite.sprite = _;
					}


				}
			}
		}
		else
			switch (PlayerBaseClass.current.playerMovement.lastValidDirection)
			{
				case PlayerMovement.PlayerDirection.None:
					break;
				case PlayerMovement.PlayerDirection.Forward:
					sprite.sprite = Up;

					break;
				case PlayerMovement.PlayerDirection.ForwardRight:
					sprite.sprite = Right;

					break;
				case PlayerMovement.PlayerDirection.Right:
					sprite.sprite = Right;

					break;
				case PlayerMovement.PlayerDirection.BackwardRight:
					sprite.sprite = Right;

					break;
				case PlayerMovement.PlayerDirection.Backward:
					sprite.sprite = Down;

					break;
				case PlayerMovement.PlayerDirection.BackwardLeft:
					sprite.sprite = Down;

					break;
				case PlayerMovement.PlayerDirection.Left:
					sprite.sprite = Left;

					break;
				case PlayerMovement.PlayerDirection.ForwardLeft:
					sprite.sprite = Left;

					break;
			}
	}
}
