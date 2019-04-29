using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    InventoryInfo _inventoryInfo;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        _inventoryInfo = go.GetComponent<InventoryInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventoryInfo.AddItem("HealtPotion", 1, (x) =>
        {

            if (PlayerBaseClass.current.playerHealth.health != PlayerBaseClass.current.playerHealth.healthMax)
            {
                PlayerBaseClass.current.playerHealth.health += _inventoryInfo.healthIncrease;
                _inventoryInfo.TryToRemoveItem();

            }
            else
            {
                //Display text ("HealthAlready full")
            }
        });
        Destroy(this.gameObject);

        
    }
}
