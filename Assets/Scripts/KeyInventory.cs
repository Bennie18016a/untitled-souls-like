using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInventory : MonoBehaviour
{
    public List<string> keys = new List<string>();

    public void AddKey(string newKey)
    {
        keys.Add(newKey);
    }

    public bool CheckKey(string key)
    {
        foreach (string _key in keys)
        {
            if (_key == key) { keys.Remove(_key); return true; }
        }
        return false;
    }
}
