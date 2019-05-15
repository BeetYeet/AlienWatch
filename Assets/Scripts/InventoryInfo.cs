using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInfo: MonoBehaviour
{
	[SerializeField]
	public List<ConsumableSlot> itemAmounts = new List<ConsumableSlot>();
	int selectedSlot;

	public int healthPotionAmount;
	public int manaPotionAmount;
	int healthPotionSlot = 1, manaPotionSlot = 1;
	public float consumeTime;
	public int healthIncrease = 10;
	public int manaIncrease = 10;
	//SuperPickup
	public int movementSpeedMultitplication = 2;
	public int DamageBoost = 30;
	PlayerMelee _playerMelee;
	public int damageModification = 20;
	public Inventory_UI IUI;
	[SerializeField]
	public SpriteWithName[] spritesWithNames;
	public Dictionary<string, Action<ConsumableSlot>> premadePotions = new Dictionary<string, Action<ConsumableSlot>>();
	public void AddPremadePotion( string name, uint amount )
	{
		Debug.Log( premadePotions.Count );
		if ( !premadePotions.ContainsKey( name ) )
		{
			Debug.LogWarning( "Unknown potion: \"" + name + "\"" );
			return;
		}
		Action<ConsumableSlot> doPotion = premadePotions[name];
		Debug.Log( "Added potion: \"" + name + "\"" );
		TryToAddAmount( name, amount, doPotion );
	}

	public GameObject slotMarker;

	[SerializeField]
	public List<Effect> activeEffects
	{
		get;
		private set;
	} = new List<Effect>();

	public void Awake()
	{
		GameObject player = GameObject.FindGameObjectWithTag( "Player" );
		_playerMelee = player.GetComponent<PlayerMelee>();
	}

	public void AddItem( uint slot, string name, uint amount, Action<ConsumableSlot> useFunction )
	{
		int index = FindIndexFromName( name );
		if ( index == -1 )
		{
			//det finns ingen slot med health potions
			itemAmounts.Add( new ConsumableSlot( name, (int) amount, (int) slot, useFunction, consumeTime ) );
			Sprite s = null;
			foreach ( SpriteWithName swn in spritesWithNames )
			{
				if ( swn.name == name )
				{
					s = swn.sprite;
					break;
				}
			}
			if ( s != null )
				IUI.EnableSlot( s, (int) slot );
		}
		else
		{
			//det finns en slot med health potions
			itemAmounts[index].amount++;
		}
	}


	public int FindIndexFromName( string name )
	{
		for ( int i = 0; i < itemAmounts.Count; i++ )
		{
			if ( itemAmounts[i].name == name )
			{
				return i;
			}
		}
		return -1;
	}
	public int FindIndexFromSlot( int slot )
	{
		for ( int i = 0; i < itemAmounts.Count; i++ )
		{
			if ( itemAmounts[i].currentSlot == slot )
			{
				return i;
			}
		}
		return -1;
	}


	public void TryToRemoveItem()
	{
		for ( int i = 0; i < itemAmounts.Count; i++ )
		{
			if ( itemAmounts[i].amount == 0 )
			{
				IUI.DisableSlot( itemAmounts[i].currentSlot );
				itemAmounts.RemoveAt( i );
			}
		}
	}
	public void TryToAddAmount( string NameOnItem, uint Amount, Action<ConsumableSlot> UseAction )
	{

		for ( int __ = 0; __ < 9; __++ )
		{
			int slot = FindIndexFromSlot( __ );
			if ( slot == -1 )
			{
				continue;
			}
			if ( NameOnItem == itemAmounts[slot].name )
			{
				itemAmounts[slot].amount++;
				if ( NameOnItem == "HealthPickup" )
				{
					healthPotionAmount = itemAmounts[slot].amount;
				}
				if ( NameOnItem == "ManaPickup" )
				{
					manaPotionAmount = itemAmounts[slot].amount;
				}
				return;
			}

		}

		for ( int __ = 0; __ < 9; __++ )
		{
			bool freeSlot = FindIndexFromSlot( __ ) == -1;
			if ( freeSlot )
			{
				AddItem( (uint) __, NameOnItem, Amount,
			 ( x ) =>
			 {
				 UseAction?.Invoke( x );
				 TryToRemoveItem();
			 }
			   );
				if ( NameOnItem == "HealthPickup" )
				{
					healthPotionSlot = itemAmounts.Count - 1;
					healthPotionAmount = (int) Amount;
				}
				if ( NameOnItem == "ManaPickup" )
				{
					manaPotionSlot = itemAmounts.Count - 1;
					manaPotionAmount = (int) Amount;
				}
				break;
			}

		}

	}

	public void AddEffect( float durationLeft, Action onStart, Action onUpdate, Action onEnd )
	{
		Effect e = new Effect( durationLeft, onStart, onUpdate, onEnd );
		GameController.curr.Tick += e.UpdateEffect;
		e.OnEnd += () => { activeEffects.Remove( e ); GameController.curr.Tick -= e.UpdateEffect; };
		activeEffects.Add( e );
	}


	void Start()
	{
		premadePotions.Add( "HealthPickup", ( x ) =>
		{
			AddEffect( 2, null, () => { PlayerBaseClass.current.playerHealth.health += (int) ( 50f / 16f ); }, null );
		} );
		premadePotions.Add( "ManaPickup", ( x ) =>
		{
			AddEffect( 2, null, () => { PlayerBaseClass.current.playerMana.mana += (int) ( 50f / 16f ); }, null );
		} );
		premadePotions.Add( "MovementSpeedPickup", ( x ) =>
		{
			AddEffect( 20f, () => { PlayerBaseClass.current.playerMovement.movementSpeed *= 1.5f; _playerMelee.meleeTime /= 2f; }, null, () => { PlayerBaseClass.current.playerMovement.movementSpeed /= 1.5f; _playerMelee.meleeTime *= 2f; } );
		} );
		premadePotions.Add( "DamageBoostPickup", ( x ) =>
		{
			AddEffect( 30f, () => { _playerMelee.damage += damageModification; _playerMelee.postMeleeCooldown *= 1.5f; _playerMelee.meleeTime *= 1.5f; }, null, () => { _playerMelee.damage -= damageModification; _playerMelee.meleeTime /= 1.5f; _playerMelee.postMeleeCooldown /= 1.5f; } );
		} );


		AddPremadePotion( "DamageBoostPickup", 5 );
		AddPremadePotion( "MovementSpeedPickup", 5 );
	}

	void Update()
	{

		TryToRemoveItem();

		if ( selectedSlot >= 10 )
		{
			selectedSlot = 0;
		}
		else if ( selectedSlot <= -1 )
		{
			selectedSlot = 9;
		}



		if ( !Input.GetKey( KeyCode.JoystickButton3 ) && !Input.GetKey( KeyCode.KeypadEnter ) )
		{
			if ( Input.GetKeyDown( KeyCode.JoystickButton4 ) )
			{
				selectedSlot--;
			}
			if ( Input.GetKeyDown( KeyCode.JoystickButton5 ) )
			{
				selectedSlot++;
			}
			#region SelectSlotsKeyboard

			if ( Input.GetKeyDown( KeyCode.Alpha1 ) )
			{
				selectedSlot = 0;
			}

			if ( Input.GetKeyDown( KeyCode.Alpha2 ) )
			{
				selectedSlot = 1;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha3 ) )
			{
				selectedSlot = 2;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha4 ) )
			{
				selectedSlot = 3;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha5 ) )
			{
				selectedSlot = 4;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha6 ) )
			{
				selectedSlot = 5;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha7 ) )
			{
				selectedSlot = 6;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha8 ) )
			{
				selectedSlot = 7;
			}
			if ( Input.GetKeyDown( KeyCode.Alpha9 ) )
			{
				selectedSlot = 8;
			}

			#endregion
		}
		if ( Input.GetKeyDown( KeyCode.JoystickButton3 ) || Input.GetKeyDown( KeyCode.KeypadEnter ) )
		{
			itemAmounts.ForEach(
				( x )
				=>
				{
					if ( x.currentSlot == selectedSlot )
					{
						x.StartConsume();
					}
				}
				);
		}

		if ( Input.GetKeyUp( KeyCode.JoystickButton3 ) || Input.GetKeyUp( KeyCode.KeypadEnter ) )
		{
			itemAmounts.ForEach(
				( x )
				=>
				{
					x.StopConsume();
				}
				);
		}

		slotMarker.transform.localPosition = IUI.sr[selectedSlot].transform.localPosition;
	}
}



[System.Serializable]
public class ConsumableSlot
{
	[SerializeField]
	public string name;
	[SerializeField]
	public int amount;
	[SerializeField]
	public int currentSlot;
	public System.Action<ConsumableSlot> use;
	private bool consuming = false;
	public float consumeTime;
	[SerializeField]
	public float consumeTimeLeft = 0f;

	public ConsumableSlot( string name, int amount, int currentSlot, Action<ConsumableSlot> use, float consumeTime )
	{
		this.name = name;
		this.amount = amount;
		this.currentSlot = currentSlot;
		this.use = use;
		this.consumeTime = consumeTime;
		GameController.curr.Tick += Tick;
	}

	public void StartConsume()
	{
		consumeTimeLeft = consumeTime;
		consuming = true;
	}
	public void StopConsume()
	{
		consumeTimeLeft = 0;
		consuming = false;
	}

	private void Tick()
	{
		float deltatime = 1f / 8f;
		if ( consuming )
		{
			consumeTimeLeft -= deltatime;
			if ( consumeTimeLeft < 0 )
			{
				use?.Invoke( this );
				StopConsume();
				amount--;
			}
		}

	}
}
[Serializable]
public class Effect
{
	[SerializeField]
	public float durationLeft
	{
		get;
		private set;
	}
	[SerializeField]
	public System.Action OnUpdate;
	[SerializeField]
	public System.Action OnEnd;

	public void UpdateEffect()
	{
		durationLeft -= 60f / GameController.curr.ticksPerMinute;
		OnUpdate?.Invoke();
		if ( durationLeft <= 0f )
		{
			OnEnd();
		}
		//Debug.Log( durationLeft );
	}

	public Effect( float durationLeft, Action onStart, Action onUpdate, Action onEnd )
	{
		this.durationLeft = durationLeft;
		onStart?.Invoke();
		OnUpdate = onUpdate;
		OnEnd = onEnd;
	}
}

[Serializable]
public class SpriteWithName
{
	[SerializeField]
	public string name;
	[SerializeField]
	public Sprite sprite;
}