using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndManaDropChance : MonoBehaviour
{
    //public int spawnChanceToDivideOneWith = 2;
    //private int spawnNumber;
    //public bool willSpawn;
    //public EnemyMelee EnemyMelee;
    //public int manaDrop;
    //public Transform thisEnemy;
    //public GameObject manaPickup;
    //public GameObject HealthPickup;
    //public Sprite Dead;


    //public void Start()
    //{
    //    spawnNumber = Random.Range(1, spawnChanceToDivideOneWith + 1);
    //    if (spawnNumber == 1)
    //    {
    //        manaDrop = Random.Range(1, 3);
    //    }

    //}

    //public void Update()
    //{
    //    thisEnemy = GetComponent<Transform>();

    //    if (spawnNumber == 1)
    //    {
    //        willSpawn = true;
    //    }
    //    else
    //    {
    //        willSpawn = false;
    //    }

    //    if (EnemyMelee.oneTimeDropSpawn == true && willSpawn == true)
    //    {
    //        if (manaDrop == 1)
    //        {
    //            Instantiate(manaPickup, thisEnemy.position, Quaternion.identity);
    //            EnemyMelee.oneTimeDropSpawn = false;
    //        }

    //        if (manaDrop == 2)
    //        {
    //            Instantiate(HealthPickup, thisEnemy.position, Quaternion.identity);
    //            EnemyMelee.oneTimeDropSpawn = false;
    //        }
    //    }

    //}

    public int dropChanceToDivideOneWith;
    private int spawnNumber;
    private bool willSpawn;
    private int manaDrop = 0;
    public GameObject ManaPickup;
    public GameObject Halthpickup;
    private Transform thisEnemy;
    private EnemyMovement enemyMovement;

    void Start()
    {
        thisEnemy = GetComponent<Transform>();
        enemyMovement = GetComponent<EnemyMovement>();
        spawnNumber = Random.Range(1, dropChanceToDivideOneWith + 1);
        if (spawnNumber == 1)
        {
            willSpawn = true;
        }
        else
        {
            willSpawn = false;
        }

        if (willSpawn == true)
        {
            manaDrop = Random.Range(1, 3);
        }
    }

    void Update()
    {
        if (manaDrop == 1 && enemyMovement.Alive == false && willSpawn == true)
        {
            Instantiate(ManaPickup, thisEnemy.position, thisEnemy.rotation);
            willSpawn = false;
        }
        else if(manaDrop == 2 && enemyMovement.Alive == false && willSpawn == true)
        {
            Instantiate(ManaPickup, thisEnemy.position, thisEnemy.rotation);
            willSpawn = false;
        }

        thisEnemy = GetComponent<Transform>();
    }

}
