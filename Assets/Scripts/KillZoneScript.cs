using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    Collider2D Collide;

    public int DamageToDo;
    public int ticksPerMana = 10;
    public int ticksLeft = 10;
    void Start()
    {
        GameController.curr.Tick += Tick;
        Collide = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageInfo _ = new DamageInfo();
        _.damage = DamageToDo;
        _.faction = Faction.ToPlayer;
        collision.GetComponent<Damageble>().DoDamage(_);
    }

    void Tick()
    {
        ticksLeft--;
        if (ticksLeft == 0)
        {
            ticksLeft = ticksPerMana;
            OnTriggerEnter2D(Collide);
        }
    }
}
