using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaHealthBar : MonoBehaviour
{
    public Image ManaBar;
    public Image HealthBar;

    public Vector2 originMana;
    public Vector2 originHealth;
    public float Size;


    void Start()
    {

    }

    void SetSize(float Mana, float Health)
    {
        float man = Mana / PlayerBaseClass.current.playerMana.maxMana;
        float hel = Health / PlayerBaseClass.current.playerHealth.healthMax;
        ManaBar.fillAmount = man;
        HealthBar.fillAmount = hel;
        
    }

    private void Update()
    {
        SetSize(PlayerBaseClass.current.playerMana.mana, PlayerBaseClass.current.playerHealth.health);
    }
}
