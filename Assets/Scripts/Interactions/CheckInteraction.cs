using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInteraction : MonoBehaviour
{
    bool inArea;

    //Sets bool "inArea" to true or false depening if the player is in the area 
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player") inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player") inArea = false;
    }

    //Returns "inArea"
    public bool IsInArea()
    {
        return inArea;
    }
    //Returns true or false, depending if the player has the key which the door needs
    public bool HasKey(string doorName)
    {
        return GameObject.FindGameObjectWithTag("Player").GetComponent<KeyInventory>().CheckKey(doorName);
    }
}
