using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreThrowHandler : MonoBehaviour
{
    bool inArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Player") return;
        inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "Player") return;
        inArea = false;
    }

    public bool InArea()
    {
        return inArea;
    }
}
