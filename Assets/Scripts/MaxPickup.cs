using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxPickup : MonoBehaviour
{
    PlayerHeath playerHealth;

    Manascript playerMana;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHeath>();
        playerMana = FindObjectOfType<Manascript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.PlayerHP < playerHealth.PlayerHPMax)
        {
            Destroy(gameObject);
            playerHealth.PlayerHP = playerHealth.PlayerHPMax;
            playerMana.Mana = playerMana.MaxMana;
        }
    }
}
