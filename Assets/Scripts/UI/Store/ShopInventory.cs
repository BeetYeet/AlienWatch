using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    [SerializeField] List<SwordObject> Items;
    [SerializeField] List<GranadeObject> Item;
    [SerializeField] Transform ItemParent;
    [SerializeField] ItemSlot[] ItemSlots;

    private void OnValidate()
    {
        if (ItemParent != null)
            ItemSlots = GetComponentsInChildren<ItemSlot>();
        RefreshUI();
    }

    void RefreshUI()
    {
        int i = 0;
        for (;i < Items.Count && i < ItemSlots.Length; i++)
        {
            ItemSlots[i].Sword = Items[i];
            ItemSlots[i].Granade = Item[i];
        }

        for (; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].Sword = null;
            ItemSlots[i].Granade = null;
        }
    }
}
