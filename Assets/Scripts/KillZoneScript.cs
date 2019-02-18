using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageInfo _ = new DamageInfo();
        _.damage = 99999;
        _.faction = Faction.Alien;
        collision.GetComponent<Damageble>().DoDamage(_);        
    }
}
