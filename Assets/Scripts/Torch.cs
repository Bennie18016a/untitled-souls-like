using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public float minIntensity = 2.0f;
    public float maxIntensity = 3.0f;
    public float Timer = 0.1f;
    private new Light light = null;

    private void Start()
    {
        light = this.GetComponentInChildren<Light>();
        InvokeRepeating("Flicker", Timer, Timer);
    }
    private void Flicker()
    {
        float R = Random.Range(minIntensity, maxIntensity);
        light.intensity = R;
    }
}