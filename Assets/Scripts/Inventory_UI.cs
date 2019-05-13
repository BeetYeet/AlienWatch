using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    [SerializeField]
    GameObject[] InventoryPlace = new GameObject[9];
    public List<Sprite> potions = new List<Sprite>();
    public Sprite ManaPotion;

    public SpriteRenderer[] sr;
    private void Awake()
    {
        potions.Add(ManaPotion);

        sr = GetComponentsInChildren<SpriteRenderer>();
        
    }

    private void Start()
    {
        foreach (SpriteRenderer _sr in sr)
        {
            _sr.enabled = false;
        }
    }
    private void Update()
    {

    }

    public void EnableSlot(Sprite s, int slot)
    {
        sr[slot].enabled = true;
        sr[slot].sprite = s;
    }
    public void DisableSlot(int slot)
    {
        sr[slot].enabled = false;
        sr[slot].sprite = null;
    }
}
