using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeAmount = 2;
    private Vector3 basePosition;
    public float shakeFactor = 0f;
    public float shakeSpeed = 3f;
    public float decayFactor = 1.1356f;
    private void Start()
    {
        basePosition = transform.position;
    }
    private void Update()
    {
        Vector3 pos = basePosition;
        pos += Vector3.up * Mathf.Sin(Time.time * 10.485248f * shakeSpeed) * shakeFactor * shakeAmount;
        pos += Vector3.right * Mathf.Sin(Time.time * 7.6525845f * shakeSpeed) * shakeFactor * shakeAmount;
        pos += Vector3.up * Mathf.Sin(Time.time * 3.2579683248f * shakeSpeed) * shakeFactor * shakeAmount;
        pos += Vector3.right * Mathf.Sin(Time.time * 2.6871634845f * shakeSpeed) * shakeFactor * shakeAmount;
        transform.position = pos;
    }

    private void FixedUpdate()
    {
        shakeFactor /= decayFactor;
    }

    public void Shake()
    {
        shakeFactor += 1f;
    }
}
