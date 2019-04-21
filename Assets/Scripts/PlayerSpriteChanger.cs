using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteChanger : MonoBehaviour
{
    Animator Anim;
    PlayerMovement playerMovement;

    private void OnValidate()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0f);

            Anim.SetFloat("Verticle", 0.1f);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Backward && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0f);
            
            Anim.SetFloat("Verticle", -0.1f);
        }
        // IDLE RIGHT
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Right && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0.1f);

            Anim.SetFloat("Verticle", 0f);
        }
        // Idle Left
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Left && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", -0.1f);

            Anim.SetFloat("Verticle", 0f);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.BackwardLeft && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0f);

            Anim.SetFloat("Verticle", -0.1f);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.BackwardRight && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0f);

            Anim.SetFloat("Verticle", -0.1f);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0f);

            Anim.SetFloat("Verticle", 1f);

        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Backward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 0f);

            Anim.SetFloat("Verticle", -1f);

        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Left && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", -1f);

            Anim.SetFloat("Verticle", 0f);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Right && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetFloat("Horizontal", 1f);

            Anim.SetFloat("Verticle", 0f);
        }

		if (playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
			SoundManager.PlaySound("RunSound");
    }
}
