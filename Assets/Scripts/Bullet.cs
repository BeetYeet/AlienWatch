using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float lifetime;
    private Transform target;
    private PlayerHeath playerHealth;
    public int damage;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHeath>();
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, -90), Space.Self);
        //Destroy(gameObject, lifetime); ???
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player") || collision.gameObject.tag == ("Wall"))
        {
            playerHealth.health -= damage;
            Destroy(gameObject);
        }
    }
}
