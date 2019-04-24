using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableKey : MonoBehaviour
{
	public bool StrengthBossKilled;
	public bool SpeedBossKilled;
	public bool VitalityBossKilled;
	SpriteRenderer SpriteRenderer;

	private void Start()
	{
		SpriteRenderer = GetComponent<SpriteRenderer>();
		SpriteRenderer.enabled = false;
	}
	private void Update()
	{
		if (SpeedBossKilled && BossDie2.redBossDead)
		{
			SpriteRenderer.enabled = true;
		}
		if (VitalityBossKilled && BossDie.GreenBossDead)
		{
			SpriteRenderer.enabled = true;
		}
		if (StrengthBossKilled && BossDie3.yellowBossDead)
		{
			SpriteRenderer.enabled = true;
		}
	}
}
