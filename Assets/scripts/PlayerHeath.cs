using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    public int PlayerHPMax = 100;
    public int PlayerHP = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHP == 0)
        {
            //die
        }
        if(PlayerHP < PlayerHPMax)
        {
            // can heal
        }
    }
}
