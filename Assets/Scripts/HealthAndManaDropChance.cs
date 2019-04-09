using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndManaDropChance : MonoBehaviour
{

    public int dropChanceToDivideOneWith;
    private int spawnNumber;
    private bool willSpawn;
    private int manaDrop = 0;
    public GameObject ManaPickup;
    public GameObject HealthPickup;
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
            Instantiate(HealthPickup, thisEnemy.position, thisEnemy.rotation);
            willSpawn = false;
        }
        if (thisEnemy == null)
        {
            Debug.Log("Transform is null");
        }

        thisEnemy = GetComponent<Transform>();
    }

}
