using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaHealthBar : MonoBehaviour
{
    [SerializeField] Transform ManaBar;
    [SerializeField] Transform HealthBar;

    [SerializeField] Vector2 originMana;
    [SerializeField] Vector2 originHealth;
    [SerializeField] float Size;


    void Start()
    {

    }

    void SetSize(float Mana, float Health)
    {
        float man = Mana / PlayerBaseClass.current.playerMana.maxMana;
        float hel = Health / PlayerBaseClass.current.playerHealth.healthMax;
        ManaBar.localScale = new Vector3(man, 1f);
        HealthBar.localScale = new Vector3(hel, 1f);
        ManaBar.localPosition = new Vector3(originMana.x - originMana.x*(1-man)/2, originMana.y);
        HealthBar.localPosition = new Vector3(originHealth.x - originHealth.x*(1-hel)/2, originHealth.y);
    }

    private void Update()
    {
        SetSize(PlayerBaseClass.current.playerMana.mana, PlayerBaseClass.current.playerHealth.health);
    }
}
