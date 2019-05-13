using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
	SpriteRenderer sr;
	public Vector2 offset;

	public List<Sprite> sprites;
	public List<GameObject> Drinks;

	

	public float velMultiplier = 50f;

	// Start is called before the first frame update
	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
			spawnDrinksRandom();
	}

	void Animate()
	{

	}

	void spawnDrinksRandom()
	{
		offset = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
		int i = Random.Range(0, Drinks.Count);
		GameObject go = Instantiate(Drinks[i], HelperClass.V3toV2(transform.position), Quaternion.identity);
		go.GetComponent<Rigidbody2D>().AddForce(offset * velMultiplier);
	}
}
