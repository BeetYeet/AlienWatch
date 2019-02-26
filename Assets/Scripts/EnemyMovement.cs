using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public Transform target;
    //public float speed = 3f;
    //public float minDist;
    //public bool agro = false;
    //private Rigidbody2D rbody;
    //public bool alive = true;
    //public bool attack = false;



    //void Start()
    //{
    //    rbody = GetComponent<Rigidbody2D>();
    //}

    //void Update()
    //{


    //    //move towards the player
    //    if (agro == true && alive == true)
    //    {
    //        transform.LookAt(target.position);
    //        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

    //        if (Vector3.Distance(transform.position, target.position) > minDist)
    //        {
    //            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    //            rbody.velocity = (PlayerBaseClass.current.transform.position - transform.position).normalized * speed;
    //            attack = false;

    //        }
    //        else
    //        {
    //            rbody.velocity = new Vector3(0, 0, 0);
    //            attack = true;
    //        }
    //    }

    //}

    private Transform target;
    public float speed;
    public float range;
    public bool agro = false;
    private Rigidbody2D rb;
    public bool alive = true;
    public bool attack = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
       if (agro == true && alive == true)
        {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            if(Vector3.Distance(transform.position, target.position) > range)
            {
                rb.velocity = (PlayerBaseClass.current.transform.position - transform.position).normalized * speed;
                attack = false;
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
                attack = true;
            }
        }
    }
}
