using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Item : ScriptableObject
{
    [Header("Inherited Item Variables")]
    public string Name, Description;
    public Rarity rarity;
    public int cost;
    public Sprite sprite;
    public int ShopStock;
}
public enum Rarity
{
    None,
    Common,
    Rare,
    Legendary
}
