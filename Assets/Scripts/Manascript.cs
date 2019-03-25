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
    private float reg = 1;

    // Start is called before the first frame update
    void Start()
    {


        //regenar 1 mana per sec
        InvokeRepeating("Regenerate", 0, 1f / reg);

    }

    // Update is called once per frame

    // lägger till +1 till Mana
    void Regenerate()
    {
        if (Mana < MaxMana)
        {
            Mana++;
        }
    }
}
