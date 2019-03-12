using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy: MonoBehaviour
{
	//public EnemyMovement EnemyMovement;
	//public GameObject Bullet;
	//public float enemySecondsBetweenAttacks;
	//public float secondsSinceLastAttack;
	//public int damage;
	//public int maxHP;
	//public int currentHP;
	//public HealthAndManaDropChance HealthAndManaDropChance;


	//void Start()
	//{
	//    GameController.curr.Tick += EnemyShoot;
	//}

	//void Update()
	//{

	//    if (EnemyMovement.attack == false)
	//    {
	//        secondsSinceLastAttack = enemySecondsBetweenAttacks;
	//    }

	//}
	//void EnemyShoot()
	//{
	//    if (secondsSinceLastAttack <= enemySecondsBetweenAttacks)
	//    {
	//        secondsSinceLastAttack += Time.deltaTime * 50;
	//    }

	//    if (EnemyMovement.attack == true && EnemyMovement.alive == true)
	//    {
	//        if (secondsSinceLastAttack >= enemySecondsBetweenAttacks)
	//        {
	//        Instantiate(Bullet, transform.position, Quaternion.identity);
	//        secondsSinceLastAttack = 0;
	//        }
	//    }
	//}

	public GameObject projectile;
	public Transform shotpoint;
	public float timeBetweenShots;
	private float shotTime;
	private EnemyMovement enemyMovement;

	public int maxHP;
	public int currentHP;

	private void Start()
	{
		enemyMovement = GetComponent<EnemyMovement>();
		GameController.curr.Tick += EnemyShoot;
		currentHP = maxHP;

	}

	private void Update()
	{
		if ( currentHP <= 0 )
		{
			enemyMovement.attack = false;
		}


	}

	public void EnemyShoot()
	{
		if ( enemyMovement.attack == true && Time.time >= shotTime && enemyMovement.Alive == true )
		{
			Instantiate( projectile, shotpoint.position, transform.rotation );
			shotTime = Time.time + timeBetweenShots;
		}
	}



}
