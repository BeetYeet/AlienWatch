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
            Anim.SetBool("IsIdleUp", false);
            Anim.SetBool("IsIdleBack", true);
            Anim.SetBool("IsIdleRight", false);
            Anim.SetBool("IsIdleLeft", false);
            // disabling Move animation
            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Backward && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsIdleUp", true);
            Anim.SetBool("IsIdleBack", false);
            Anim.SetBool("IsIdleRight", false);
            Anim.SetBool("IsIdleLeft", false);

            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Right && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsIdleUp", false);
            Anim.SetBool("IsIdleBack", false);
            Anim.SetBool("IsIdleRight", false);
            Anim.SetBool("IsIdleLeft", true);

            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Left && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsIdleUp", false);
            Anim.SetBool("IsIdleBack", false);
            Anim.SetBool("IsIdleRight", true);
            Anim.SetBool("IsIdleLeft", false);

            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.BackwardLeft && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsIdleUp", false);
            Anim.SetBool("IsIdleBack", false);
            Anim.SetBool("IsIdleRight", true);
            Anim.SetBool("IsIdleLeft", false);

            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }
        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.BackwardRight && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsIdleUp", false);
            Anim.SetBool("IsIdleBack", false);
            Anim.SetBool("IsIdleRight", false);
            Anim.SetBool("IsIdleLeft", true);

            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", true);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Backward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsMovingUp", true);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Left && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", false);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", true);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }

        if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            Anim.SetBool("IsMovingUp", false);
            Anim.SetBool("IsMovingBack", true);
            Anim.SetBool("IsMovingLeft", false);
            Anim.SetBool("IsMovingRight", false);
        }



    }
}
