using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTheItem : MonoBehaviour
{
    ShopInventory shopInventory;
    public Item item;
    public Gold gold;

    private void OnValidate()
    {
        shopInventory = GameObject.FindGameObjectWithTag("Shop").GetComponent<ShopInventory>();
        shopInventory.Items[3] = null;
        while (shopInventory.Items.Count > 3)
            shopInventory.Items.RemoveAt(shopInventory.Items.Count - 1);
    }

    private void Update()
    {

        if (gold.gold >= item.cost)
        {

        }
        else
        {

        }
    }
    public void OnBuy(bool Active)
    {
        if (item.ShopStock > 0)
        {
            Active = true;
        }
        if (Active == true)
        {
            if (gold.gold >= item.cost)
            {
                gold.gold -= item.cost;
                item.ShopStock -= 1;
                if (item.ShopStock <= 0)
                    Active = false;
                Debug.Log(string.Format("you bought {0}!", item.name));
            }
            else
            {
                Debug.Log("You cant buy!");
            }
        }
        if (Active == false)
        {
            Debug.LogWarning(string.Format("{0} is out of stock!", item.name));
        }
    }
}
