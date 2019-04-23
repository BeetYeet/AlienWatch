using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashdust : MonoBehaviour
{
    public Transform Point;
    PlayerMovement MoveS;
    public GameObject pSystem;

    //TimeTest
    public bool stopSpawning = true;
    public float spawnTime;
    public float spawnDelay;

    public int ticksPerSpawn;
    public int ticksSoFar;

    // Start is called before the first frame update
    void Start()
    {
        MoveS = PlayerBaseClass.current.playerMovement;

        GameController.curr.Tick += () =>
        {
            if (stopSpawning)
                return;

            ticksSoFar++;
            if (ticksSoFar >= ticksPerSpawn)
            {
                ticksSoFar -= ticksPerSpawn;
                SpawnObjekt();
            }
        };


    }

    public void SpawnObjekt()
    {
        Instantiate(pSystem, Point.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()

    {
        if (MoveS.movementState == PlayerMovement.PlayerMovementState.Moving)
        {
            stopSpawning = false;
        }
        else
        {
            stopSpawning = true;
        }
    }
}
