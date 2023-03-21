using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteraction : MonoBehaviour
{
    bool inArea;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player") inArea = true;
    }

    public bool IsInArea()
    {
        return inArea;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Player") inArea = false;
    }
}
