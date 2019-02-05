using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : Damageble
{
    public int PlayerHPMax = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            //die
        }
        if (health < PlayerHPMax)
        {
            // can heal
        }
    }
    public override void DoDamage(DamageInfo info)
    {

    }
}

public struct DamageInfo
{
    Faction faction;
    int damage;
}
public enum Faction
{
    Player,
    Alien
}
