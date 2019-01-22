using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manascript : MonoBehaviour
{
    [Header("Mana")]
    [Space]

    public int MaxMana = 10;
    public int Mana = 10;

    [SerializeField]
    private int reg = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameController.curr.Tick += Tick;
    }

    void Tick()
    {
        if (Mana < MaxMana)
        {
            MaxMana ++;
            if (Mana > MaxMana)
            {
                Mana = MaxMana;
            }
        }
    }

}
