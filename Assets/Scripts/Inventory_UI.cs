using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_UI: MonoBehaviour
{
	[SerializeField]
	GameObject[] InventoryPlace;

	public SpriteRenderer[] sr;
	public TextMeshProUGUI[] numbers;
	private void Awake()
	{
		sr = GetComponentsInChildren<SpriteRenderer>();
		foreach ( SpriteRenderer _sr in sr )
		{
			_sr.enabled = false;
		}
		numbers = GetComponentsInChildren<TextMeshProUGUI>();
		foreach ( TextMeshProUGUI txt in numbers )
		{
			txt.text = "";
		}
	}

	private void Update()
	{

	}

	public void EnableSlot( Sprite s, int slot )
	{
		sr[slot].enabled = true;
		sr[slot].sprite = s;
	}
	public void DisableSlot( int slot )
	{
		sr[slot].enabled = false;
		sr[slot].sprite = null;
	}

	internal void Display( int slot, uint amount )
	{
		numbers[slot].text = amount.ToString();
	}
	internal void DontDisplay( int slot )
	{
		numbers[slot].text = "";
	}
}
