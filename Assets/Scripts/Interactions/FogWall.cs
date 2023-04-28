using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    public GameObject boss;

    //Once the boss has been killed, removes all of the fogwalls linked to the boss
    private void Update()
    {
        if (boss.GetComponent<BossStateMachine>().dead)
        {
            Destroy(gameObject);
        }
    }

}
