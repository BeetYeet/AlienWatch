using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : Damageble
{
	public override void DoDamage(DamageInfo info)
	{
		TriggerDestroy();
	}

	private void TriggerDestroy()
	{
		
	}
}
