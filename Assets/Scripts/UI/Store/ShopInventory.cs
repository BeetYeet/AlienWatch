using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public List<Item> Items;
    public Transform ItemParent;
    public ItemSlot[] ItemSlots;

    private void OnValidate()
    {
        if (ItemParent != null)
            ItemSlots = GetComponentsInChildren<ItemSlot>();
        RefreshUI();
    }

    void RefreshUI()
    {
        int i = 0;
        for (; i < Items.Count && i < ItemSlots.Length; i++)
        {
            ItemSlots[i].Sword = Items[i];

        }

        for (; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].Sword = null;

        }
    }
}
