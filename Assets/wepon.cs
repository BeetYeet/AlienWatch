using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wepon : MonoBehaviour
{
    
    public Transform FirePoint;
    public GameObject grenadePrefab;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
       
    }
         
       


        

    void Shoot()
    {
       Instantiate(grenadePrefab, FirePoint.position, transform.rotation);
    }
    //Quaternion.identity


}
