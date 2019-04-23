using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDie : MonoBehaviour
{
	public EnemyHealth EnemyHealth;
	public bool GreenBossDead;
    // Update is called once per frame
    void Update()
    {
		if (EnemyHealth.health <= 0)
			GreenBossDead = true;

    }
}
