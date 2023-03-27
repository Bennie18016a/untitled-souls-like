using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    public GameObject boss;

    private void Update()
    {
        if (boss.GetComponent<BossStateMachine>().dead)
        {
            Destroy(gameObject);
        }
    }

}
