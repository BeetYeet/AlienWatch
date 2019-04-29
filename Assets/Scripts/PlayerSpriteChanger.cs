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
		if (playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
			SoundManager.PlaySound("RunSound");
		// forward Idle
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", true);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", false);
		}
		// back Idle
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Backward && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", true);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", false);
		}
		// IDLE RIGHT
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Right && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", true);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", false);
		}
		// Idle Left
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Left && playerMovement.movementState != PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", true);
		}

		// forward Moving
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Forward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", true);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", false);

		}
		// back Moving
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Backward && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", true);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", false);

		}
		// left Moving
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Left && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", true);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", false);
			Anim.SetBool("SideIdleRight", false);
		}
		// Right Moving
		if (playerMovement.lastValidDirection == PlayerMovement.PlayerDirection.Right && playerMovement.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
			Anim.SetBool("frontIdle", false);
			Anim.SetBool("FrontMoving", false);
			Anim.SetBool("SideMovingLeft", false);
			Anim.SetBool("SideIdle", false);
			Anim.SetBool("BackIdle", false);
			Anim.SetBool("BackMoving", false);
			Anim.SetBool("SideMovingRight", true);
			Anim.SetBool("SideIdleRight", false);

		}

		
    }
}
