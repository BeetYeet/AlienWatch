
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper: MonoBehaviour
{
	public Animator anim;
	float lastX = 0f;
	float lastY = -.25f;
	void Update()
	{
		if ( Time.timeScale == 0f )
		{
			anim.SetFloat( "X", 0f );
			anim.SetFloat( "Y", -.25f );
			return;
		}
		float X = Input.GetAxis( "Horizontal" );
		float Y = Input.GetAxis( "Vertical" );
		if ( Mathf.Abs( X ) > .20f || Mathf.Abs( Y ) > .20f )
		{
			lastX = X;
			lastY = Y;
		}
		anim.SetFloat( "X", lastX );
		anim.SetFloat( "Y", lastY );
	}
}
