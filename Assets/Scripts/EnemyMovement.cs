using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 4;
    public int maxDist = 10;
    public int minDist = 5;
    public Sprite EnemySprite;




    void Start()
    {
        
    }

    void Update()
    {
        transform.LookAt((player));

       if (Vector3.Distance(transform.position, player.position) >= minDist)
        {

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
            //transform.position = Vector2.MoveTowards(transform.position, (Vector2)player.position, moveSpeed);


            if (Vector3.Distance(transform.position, player.position) <= maxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = EnemySprite;
    }
}
