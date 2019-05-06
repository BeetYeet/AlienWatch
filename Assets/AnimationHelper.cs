
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
	public Animator anim;
	float lastX = 0f;
	float lastY = -.25f;
	void Update()
	{
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
