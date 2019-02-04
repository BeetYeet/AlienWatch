using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    PlayerHeath playerHeath;
    public GameObject Deathscreen;

    private void Start()
    {
        playerHeath = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
        Deathscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHeath.PlayerHP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Deathscreen.SetActive(true);
    }
}
