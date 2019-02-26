using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Sword", fileName = "new Sword")]
[System.Serializable]
public class SwordObject : Item
{
    [Header("Floats")]
    [Space]
    public float MeleeTime;
    public float RotationLeft;
    public float RotationRight;
    [Header("Ints")]
    [Space]
    public int Damage;
    [Header("Vectors")]
    [Space]
    public Vector3 defaultPos;
    public Vector3 forwardPos;
    public Vector3 swipeLeftPos;
    public Vector3 SwipeRightPos;
    [Header("Sword Return")]
    [Space]
    public SwordReturn Return;
    public HitEffectData hitEffectData;


}

public class HitEffectData
{

}

