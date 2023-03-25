using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteraction : MonoBehaviour
{
    bool inArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") inArea = true;
    }

    public bool IsInArea()
    {
        return inArea;
    }

    public bool HasKey(string doorName)
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>().CheckKey(doorName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player") inArea = false;
    }
}
