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

        //Give the player any items
        foreach (Item item in items)
        {
            itemMenu.AddToList(item);

            if (item.itemType == Item.ItemType.key)
            {
                player.GetComponent<KeyInventory>().AddKey(item.name, item);
            }
        }

        if (items.Count > 0)
            itemMenu.gameObject.SetActive(true);

        //Add currency to player
        player.GetComponent<KeyInventory>().AddCurrency(currency);
    }
}
