using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPickup : MonoBehaviour
{
    InventoryInfo _inventoryInfo;

    private void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("GameController");

        _inventoryInfo = go.GetComponent<InventoryInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventoryInfo.TryToAddAmount("MovementSpeedPickup", (uint)1, (x) =>
        {
            PlayerBaseClass.current.playerMovement.movementSpeed *= _inventoryInfo.movementSpeedMultitplication;
        });
        Destroy(this.gameObject);
    }
}
