using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadAutoDestroy : MonoBehaviour
{
    public float WhenToDestroy;
    float time;

    private void Update()
    {
        if (time > WhenToDestroy)
        {
            Destroy(gameObject);
        }

        time += 1 * Time.deltaTime;
    }
}
