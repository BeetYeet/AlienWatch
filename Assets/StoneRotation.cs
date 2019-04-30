using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneRotation : MonoBehaviour
{
    Vector2 dir = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        dir = PlayerMovement.GetVectorDirection(PlayerBaseClass.current.playerMovement.playerDir);
        transform.localPosition = dir;
    }
}
