using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manascript : MonoBehaviour
{
    [Header("Mana")]
    [Space]

    public int MaxMana = 10;
    public int Mana = 10;

    

    // Start is called before the first frame update
    void Start()
    {
        GameController.curr.Tick += Tick;
    }

    void Tick()
    {
      if(Mana < MaxMana)
        {
            Mana++;
        }
      if(Mana == MaxMana)
        {
            Mana = MaxMana;
        }
    }

}
