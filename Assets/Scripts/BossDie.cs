using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
	public EnemyHealth EnemyHealth;
	public static bool GreenBossDead;
    // Update is called once per frame
    void Update()
    {
		EnemyHealth.OnDeath +=
		() =>
		{
			GreenBossDead = true;
			BossKilledText.curr.text.text = BossKilledText.curr.VitalityBoss;
			BossKilledText.curr.time3 = BossKilledText.curr.waitToRem;
		};
	}
}
