using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie2 : MonoBehaviour
{
	public EnemyHealth EnemyHealth;
	public static bool redBossDead;
	// Update is called once per frame
	void Update()
	{
		EnemyHealth.OnDeath +=
		() =>
			{
				redBossDead = true;
			};
	}
}
