using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPickup : MonoBehaviour
{

    InventoryInfo _inventoryInfo;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        _inventoryInfo = go.GetComponent<InventoryInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventoryInfo.TryToAddAmount("ManaPickup", (uint)1, (x) =>
        {
            _inventoryInfo.AddEffect(2, null, () => { PlayerBaseClass.current.playerMana.mana += 50 / 16; }, null);

        });
        Destroy(this.gameObject);
    }

}




