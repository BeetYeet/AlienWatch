using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    Manascript playerMana;

    public int ManaBonus = 20;

    private void Awake()
    {
        playerMana = FindObjectOfType<Manascript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerMana.Mana < playerMana.MaxMana)
        {
            Destroy(gameObject);
            playerMana.Mana = playerMana.Mana + ManaBonus;
        }
    }



}
