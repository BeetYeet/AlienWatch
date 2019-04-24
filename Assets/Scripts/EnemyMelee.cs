using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee: MonoBehaviour
{
	//public float enemySecondsBetweenAttacks;
	//public float secondsSinceLastAttack;
	//public int enemyDamage;
	//public PlayerHeath playerHealth;
	//public int enemyMeleeCurrentHealth;
	//public int enemyMeleeMaxHealth;
	//public bool oneTimeDropSpawn = false;
	//public bool playerinsidehitbox = false;
	//public EnemyMovement EnemyMovement;

	//public Sprite deadEnemySprite;

	//public void Start()
	//{
	//    enemyMeleeCurrentHealth = enemyMeleeMaxHealth;
	//    GameController.curr.Tick += DoDamage;
	//}

	//public void Update()
	//{
	//    if (enemyMeleeCurrentHealth <= 0 && enemyMeleeCurrentHealth != -398)
	//    {
	//        GetComponent<SpriteRenderer>().sprite = deadEnemySprite;
	//        oneTimeDropSpawn = true;
	//        EnemyMovement.alive = false;
	//        enemyMeleeCurrentHealth = -398;

	//    }
	//}


	//private void OnTriggerEnter2D(Collider2D collision)
	//{
	//    if (collision.tag == "Player")
	//    {
	//        playerinsidehitbox = true;
	//    }

	//}

	//private void OnTriggerExit2D(Collider2D collision)
	//{
	//    secondsSinceLastAttack = enemySecondsBetweenAttacks;
	//    playerinsidehitbox = false;
	//}

	//public void DoDamage()
	//{
	//    if (playerinsidehitbox == true && EnemyMovement.alive == true && EnemyMovement.attack == true)
	//    {
	//        if (secondsSinceLastAttack >= enemySecondsBetweenAttacks)
	//        {
	//            secondsSinceLastAttack = 0;
	//            playerHealth.PlayerHP -= enemyDamage;
	//        }
	//    }
	//    if (secondsSinceLastAttack <= enemySecondsBetweenAttacks)
	//    {
	//        secondsSinceLastAttack += Time.deltaTime * 50;
	//    }
	//}

	public int damage;
	HealthAndManaDropChance HealthAndManaDropChance;
	public float timeBetweenAttacks;
	private float lastAttack = 0f;
	EnemyMovement enemyMovement;
	public float strength = 1f;

	void Start()
	{
		HealthAndManaDropChance = GetComponent<HealthAndManaDropChance>();
		enemyMovement = GetComponent<EnemyMovement>();
		GameController.curr.Tick += DoDamage;
	}
	private void Update()
	{

	}

	public void DoDamage()
	{
		if ( enemyMovement.attack && enemyMovement.Alive )
		{
			if ( Time.time > lastAttack + timeBetweenAttacks )
			{
				DamageInfo di = new DamageInfo( Faction.ToPlayer, damage );
				PlayerBaseClass.current.playerHealth.DoDamage( di );
				lastAttack = Time.time;
				Vector2 dir = HelperClass.V3toV2( PlayerBaseClass.current.transform.position ) - HelperClass.V3toV2( transform.position );
				PlayerBaseClass.current.playerMovement.DoKnockback( dir.normalized * strength );
			}
		}
		else
		{
			lastAttack = Time.time - timeBetweenAttacks * 1 / 3;
		}
	}
}
