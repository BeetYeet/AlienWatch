using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	AudioSource As;
	public AudioClip AudioClip;
	SpriteRenderer sr;
	public Vector2 offset;

	public Sprite sprites;
	public List<GameObject> Drinks;

	public bool opened = false;

	public float velMultiplier = 50f;


	// Start is called before the first frame update
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		As = GetComponent<AudioSource>();
		GameController.curr.ChangeTraversable(GameController.ClampToGrid(transform.position), false);
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
		int SpawnAmount = Random.Range(1, 4);
		Debug.Log("spawn amount: " + SpawnAmount);
		if (SpawnAmount == 1)
		{
			int i = Random.Range(0, Drinks.Count);

			Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));

			GameObject go = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
			go.GetComponent<Rigidbody2D>().AddForce(_offset * velMultiplier);
		}
		if (SpawnAmount == 2)
		{
			{
				int i = Random.Range(0, Drinks.Count);

				Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));

				GameObject go1 = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
				Debug.Log("go1 name: " + go1.name);
				go1.GetComponent<Rigidbody2D>().AddForce(_offset * velMultiplier);
			}
			{
				int i = Random.Range(0, Drinks.Count);

				Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));

				GameObject go2 = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
				go2.GetComponent<Rigidbody2D>().AddForce(_offset * velMultiplier);
			}
		}
		if (SpawnAmount == 3)
		{
			{
				int i = Random.Range(0, Drinks.Count);

				Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));

			GameObject go1 = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
			go1.GetComponent<Rigidbody2D>().AddForce(_offset * velMultiplier);
			}
			{
				int i = Random.Range(0, Drinks.Count);

				Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));

			GameObject go2 = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
			go2.GetComponent<Rigidbody2D>().AddForce(_offset * velMultiplier);
			}
			{
				int i = Random.Range(0, Drinks.Count);

				Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));

			GameObject go3 = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
			go3.GetComponent<Rigidbody2D>().AddForce(_offset * velMultiplier);
			}
		}

	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == PlayerBaseClass.current.gameObject && !opened)
		{
			opened = true;
			As.PlayOneShot(AudioClip);
		}
		else if (collision.gameObject == PlayerBaseClass.current.gameObject && opened)
			return;
		else if (collision.gameObject != PlayerBaseClass.current.gameObject)
		{
			return;
		}
		if (opened)
		{
			spawnDrinksRandom();
			sr.sprite = sprites;
		}
	}
}
