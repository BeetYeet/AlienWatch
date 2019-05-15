using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie3 : MonoBehaviour
{
	public EnemyHealth EnemyHealth;
	public static bool yellowBossDead;
	// Update is called once per frame
	void Update()
	{
		EnemyHealth.OnDeath +=
		() =>
		{
			yellowBossDead = true;
			BossKilledText.curr.text.text = BossKilledText.curr.StrengthBoss;
			BossKilledText.curr.time2 = BossKilledText.curr.waitToRem;
		};
	}
}
