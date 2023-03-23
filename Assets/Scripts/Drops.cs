using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public List<string> keys = new List<string>();
    public int currency;

    public void Died()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (string key in keys)
        {
            player.GetComponent<KeyInventory>().AddKey(key);
        }

        //add currency to player
    }
}
