using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxPickup : MonoBehaviour
{
    PlayerHeath playerHealth;

    PlayerMana playerMana;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHeath>();
        playerMana = FindObjectOfType<PlayerMana>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.PlayerHP < playerHealth.PlayerHPMax)
        {
            Destroy(gameObject);
            playerHealth.PlayerHP = playerHealth.PlayerHPMax;
            playerMana.mana = playerMana.maxMana;
        }
    }
}
