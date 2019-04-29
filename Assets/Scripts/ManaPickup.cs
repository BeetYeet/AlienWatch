using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{
    private string potionName = "ManaPotion";
    

    InventoryInfo _inventoryInfo;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        _inventoryInfo = go.GetComponent<InventoryInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventoryInfo.AddItem("ManaPotion", 1, (x) =>
        {
            if (PlayerBaseClass.current.playerMana.mana != PlayerBaseClass.current.playerMana.maxMana)
            {
                PlayerBaseClass.current.playerMana.mana += _inventoryInfo.manaIncrease;
                _inventoryInfo.TryToRemoveItem();
            }
            else
            {
                //MANA ÄR FULL

            }
        });
        Destroy(this.gameObject);
    }

}




