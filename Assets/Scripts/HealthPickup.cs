using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerHeath playerHealth;

    public int HealthBonus = 10;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHeath>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.health < playerHealth.healthMax)
        {
            Destroy(gameObject);
            playerHealth.health = playerHealth.healthMax + HealthBonus;
        }
    }
}
