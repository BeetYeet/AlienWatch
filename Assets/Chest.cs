using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest: MonoBehaviour
{
	AudioSource As;
	public AudioClip AudioClip;
	SpriteRenderer sr;
	public Vector2 offset;

	public Sprite sprites;
	[SerializeField]
	public List<Drop> Drinks;

	public bool opened = false;

	public float velMultiplier = 50f;
	public int minSpawn, maxSpawn = 2;

	// Start is called before the first frame update
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		As = GetComponent<AudioSource>();
		GameController.curr.ChangeTraversable( GameController.ClampToGrid( transform.position ), false );
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Animate()
	{

	}

	void spawnDrinksRandom()
	{
		int SpawnAmount = Random.Range( minSpawn, maxSpawn );
		Debug.Log( "spawn amount: " + SpawnAmount );
		for ( int i = 0; i < SpawnAmount; i++ )
		{
			Drop highestDrop = null;
			float highestVal = Mathf.NegativeInfinity;
			for ( int p = 0; p < Drinks.Count; p++ )
			{
				Drop pot = Drinks[p];
				float val = Random.value * pot.weight;
				if ( highestVal < val )
				{
					highestVal = val;
					highestDrop = pot;
				}
			}
			Vector2 _offset = new Vector2( Random.Range( -offset.x, offset.x ), Random.Range( -offset.y, offset.y ) );
			GameObject go = Instantiate( highestDrop.prefab, HelperClass.V3toV2( transform.position ), Quaternion.identity );
			go.GetComponent<Rigidbody2D>().AddForce( _offset * velMultiplier );
		}

	}


	private void OnCollisionEnter2D( Collision2D collision )
	{
		if ( collision.gameObject == PlayerBaseClass.current.gameObject && !opened )
		{
			opened = true;
			As.PlayOneShot( AudioClip );
		}
		else if ( collision.gameObject == PlayerBaseClass.current.gameObject && opened )
			return;
		else if ( collision.gameObject != PlayerBaseClass.current.gameObject )
		{
			return;
		}
		if ( opened )
		{
			spawnDrinksRandom();
			gameObject.layer = LayerMask.NameToLayer( "Terrain_Passable" );
			sr.sprite = sprites;
		}
	}
}

[System.Serializable]
public class Drop
{
[SerializeField]
	private string name
	{
		get
		{
			if ( prefab == null )
			{
				return "ITEM";
			}
			return prefab.name;
		}
	}
	[SerializeField]
	public float weight = 1f;
	[SerializeField]
	public GameObject prefab;
}