using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie3 : MonoBehaviour
{
	public EnemyHealth EnemyHealth;
	public bool yellowBossDead;
	// Update is called once per frame
	void Update()
	{
		if (EnemyHealth.health <= 0)
			yellowBossDead = true;

	}
}
