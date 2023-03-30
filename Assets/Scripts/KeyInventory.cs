using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeyInventory : MonoBehaviour, IDataPersistence
{
    public List<string> keys = new List<string>();
    public List<Item> itemKeys = new List<Item>();
    public Inventory inv;

    public void AddKey(string newKey, Item itemKey)
    {
        keys.Add(newKey);
        itemKeys.Add(itemKey);
        inv.AddItem(itemKey);
    }

    public bool CheckKey(string key)
    {
        foreach (string _key in keys)
        {
            if (_key == key)
            {
                keys.Remove(_key);
                inv.RemoveItem(itemKeys.Find(Item => Item.name == _key));
                itemKeys.RemoveAll(Item => Item.name == _key);
                return true;
            }
        }
        return false;
    }

    public void LoadData(GameData data)
    {
        for (int i = 0; i != data.Keys.Count; i++)
        {
            data.Keys.TryGetValue(i.ToString(), out string key);
            keys.Add(key);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.Keys.Clear();
        for (int i = 0; i != keys.Count; i++)
        {
            data.Keys.Add(i.ToString(), keys[i]);
        }
    }
}
