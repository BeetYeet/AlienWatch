using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroPoint : MonoBehaviour
{
    EnemyMovement enemyMovement;
    EnemyMovement Enemymove;
    private void Start()
    {
        enemyMovement = GameObject.FindGameObjectWithTag("EnemyRanged").GetComponent<EnemyMovement>();
        Enemymove = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemyMovement.agro = true;
            Enemymove.agro = true;
            
        }
    }
}
