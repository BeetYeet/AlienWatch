using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageble : MonoBehaviour
{
    public int health;
    public abstract void DoDamage(DamageInfo info);
}
