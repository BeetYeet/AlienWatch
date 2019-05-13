using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
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
		Vector2 _offset = new Vector2(Random.Range(-offset.x, offset.x), Random.Range(-offset.y, offset.y));
		int i = Random.Range(0, Drinks.Count);
		GameObject go = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
		go.GetComponent<Rigidbody2D>().AddForce(offset * velMultiplier);
	}


	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == PlayerBaseClass.current.gameObject && !opened)
		{
			opened = true;
		}
		else if (collision.gameObject == PlayerBaseClass.current.gameObject && opened)
			return;
		else if(collision.gameObject != PlayerBaseClass.current.gameObject)
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
