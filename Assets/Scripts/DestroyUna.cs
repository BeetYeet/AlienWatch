using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUna : MonoBehaviour
{
	public State state;
	public GameObject Particle;

	public Sprite NotDestroyed, destroyed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	public enum State
	{
		NotDestroyed,
		destroyed
	}
}
