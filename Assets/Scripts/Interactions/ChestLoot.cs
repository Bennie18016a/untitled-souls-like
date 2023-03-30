using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    public List<Item> Items = new List<Item>();
    public List<QuickItem> quickItems = new List<QuickItem>();
    public ItemMenu itemMenu;

    public void OpenChest()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (Item item in Items)
        {
            if (item.itemType == Item.ItemType.key)
            {
                player.GetComponent<KeyInventory>().AddKey(item.name, item);
            }
        }

        foreach (QuickItem quickItem in quickItems)
        {
            player.GetComponent<UseQuickItem>().QuickItems[quickItem.ID].Number++;
            itemMenu.AddToList(quickItem);
        }

        GetComponent<Animator>().SetTrigger("Open");
        itemMenu.gameObject.SetActive(true);
        Destroy(this);
    }
}