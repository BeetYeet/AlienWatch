using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoostPickup : MonoBehaviour
{
    InventoryInfo _inventoryInfo;
    PlayerMelee _playerMelee;

    float TimeLeft = 3;
    float PotionDuration = 5;


    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");
        _inventoryInfo = go.GetComponent<InventoryInfo>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _playerMelee = player.GetComponent<PlayerMelee>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventoryInfo.TryToAddAmount("DamageBoostPickup", (uint)1, (x) =>
        {
            while (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
            }
            _playerMelee.damage += _inventoryInfo.DamageBoost;



        });

        Destroy(this.gameObject);


    }

    
}
