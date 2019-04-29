using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInfo : MonoBehaviour
{
    public List<ConsumableSlot> itemAmounts = new List<ConsumableSlot>();
    int selectedSlot;

    ManaPickup _manaPickup;
    HealthPickup _healthPickup;
    int healthPotionAmount
    {
        get
        {
            int i = FindIndex("HealthPotion");
            return itemAmounts[i].amount;
        }
    }
    int manaPotionAmount
    {
        get
        {
            int i = FindIndex("ManaPotion");
            return itemAmounts[i].amount;
        }
    }
    int healthPotionSlot, manaPotionSlot;
    public float consumeTime;
    public int healthIncrease = 10;
    public int manaIncrease = 10;
    //SuperPickup

    public void Awake()
    {

    }

    public void AddItem(string name, uint amount, Action<ConsumableSlot> useFunction)
    {
        int index = FindIndex(name);
        if (index == -1)
        {
            //det finns ingen slot med health potions
            itemAmounts.Add(new ConsumableSlot(name, 1, itemAmounts.Count, useFunction, consumeTime));
        }
        else
        {
            //det finns en slot med health potions
            itemAmounts[index].amount++;
        }
    }


    public int FindIndex(string name)
    {
        for (int i = 0; i < itemAmounts.Count; i++)
        {
            if (itemAmounts[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }

    public void TryToRemoveItem()
    {
        for (int i = 0; i < itemAmounts.Count; i++)
        {
            if (itemAmounts[i].amount <= 0)
            {
                itemAmounts.RemoveAt(i);
            }
        }
    }

    void Start()
    {
        AddItem("HealtPotion", 1, (x) =>
        {

            if (PlayerBaseClass.current.playerHealth.health != PlayerBaseClass.current.playerHealth.healthMax)
            {
                PlayerBaseClass.current.playerHealth.health += healthIncrease;
                TryToRemoveItem();

            }
            else
            {
                //Display text ("HealthAlready full")
            }
        });
        AddItem("ManaPotion", 1, (x) =>
        {
            if (PlayerBaseClass.current.playerMana.mana != PlayerBaseClass.current.playerMana.maxMana)
            {
                PlayerBaseClass.current.playerMana.mana += manaIncrease;
                TryToRemoveItem();
            }
            else
            {
                //MANA ÄR FULL

            }
        });

    }

    void Update()
    {
        

        if (!Input.GetKey(KeyCode.KeypadEnter))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedSlot = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                selectedSlot = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                selectedSlot = 2;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            itemAmounts.ForEach(
                (x)
                =>
                {
                    if (x.currentSlot == selectedSlot)
                    {
                        x.StartConsume();
                    }
                }
                );
        }

        if (Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            itemAmounts.ForEach(
                (x)
                =>
                {
                    x.StopConsume();
                }
                );
        }

    }
}




public class ConsumableSlot
{
    public string name;
    public int amount;
    public int currentSlot;
    public System.Action<ConsumableSlot> use;
    private bool consuming = false;
    public float consumeTime;
    public float consumeTimeLeft = 0f;

    public ConsumableSlot(string name, int amount, int currentSlot, Action<ConsumableSlot> use, float consumeTime)
    {
        this.name = name;
        this.amount = amount;
        this.currentSlot = currentSlot;
        this.use = use;
        this.consumeTime = consumeTime;
        GameController.curr.Tick += Tick;
    }

    public void StartConsume()
    {
        consumeTimeLeft = consumeTime;
        consuming = true;
    }
    public void StopConsume()
    {
        consumeTimeLeft = 0;
        consuming = false;
    }

    private void Tick()
    {
        float deltatime = 1f / 8f;
        if (consuming)
        {
            consumeTimeLeft -= deltatime;
            if (consumeTimeLeft < 0)
            {
                use(this);
                consumeTimeLeft = 0;
                consuming = false;
                amount--;
            }
        }

    }
}

/*public class Potion : ConsumableSlot
{

}*/
