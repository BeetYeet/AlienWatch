using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : MonoBehaviour
{
    
    public Transform FirePoint;
    public GameObject grenadePrefab;
    // Update is called once per frame
    void Update()
    {
        if (InputHandler.current.GetWithName("Grenade").GetButtonDown())
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
