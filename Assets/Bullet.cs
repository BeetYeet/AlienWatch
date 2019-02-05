using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explotionPrefab;
    public float timeToExplode = 2f;
    public float speed = 20f;
    private Rigidbody2D bulletRB;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();

        bulletRB.velocity = transform.up * speed;
    }

    private void Update()
    {
        timeToExplode -= Time.deltaTime;

        if (timeToExplode <= 0f)
        {
            Destroy(gameObject);
            Instantiate(explotionPrefab, transform.position, Quaternion.identity);
        }
    }
}