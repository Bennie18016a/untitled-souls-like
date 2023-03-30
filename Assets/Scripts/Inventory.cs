using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public List<QuickItem> quickItems = new List<QuickItem>();
    public GameObject invPrefab;
    public Transform inventory;

    public void AddItem(Item item)
    {
        if (items.Contains(item)) return;
        items.Add(item);
        GameObject inventoryItem = GameObject.Instantiate(invPrefab, Vector3.zero, Quaternion.identity);
        inventoryItem.GetComponentInChildren<TMP_Text>().text = item.name;
        inventoryItem.transform.name = string.Format("{0} inv", item.name);
        inventoryItem.transform.GetChild(1).GetComponent<Image>().sprite = item.Icon;
        inventoryItem.transform.SetParent(inventory);
    }

    public void RemoveItem(Item item)
    {
        if (!items.Contains(item)) return;
        for (int i = 0; i != inventory.childCount; i++)
        {
            if (inventory.GetChild(i).name == string.Format("{0} inv", item.name))
            {
                GameObject.Destroy(inventory.GetChild(i).gameObject);
            }
        }
        items.Remove(item);
    }
}
