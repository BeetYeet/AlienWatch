using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{
    public bool ParticleTrigger = true;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }




    // Update is called once per frame
    void Update()
    {

    }
}
