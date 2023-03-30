using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour
{
    public ItemMenu itemMenu;
    public List<Item> items = new List<Item>();
    public int currency;

    public void Died()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (Item item in items)
        {
            itemMenu.AddToList(item);

            if (item.itemType == Item.ItemType.key)
            {
                player.GetComponent<KeyInventory>().AddKey(item.name, item);
            }
        }

        itemMenu.gameObject.SetActive(true);
        //add currency to player
    }
}
