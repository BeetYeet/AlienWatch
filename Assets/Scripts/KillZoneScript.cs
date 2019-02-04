using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    PlayerHeath playerHeath;

    private void Start()
    {
        playerHeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHeath.PlayerHP = 0;
        }
    }
}
