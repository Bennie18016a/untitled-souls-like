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
        /*When opening the chest, to give the player their items I:
        Store the player as a variable
        Add the key to the key inventory, if there is any,
        Add the quickItem to their inventory and send it to the script "itemMenu" so UI can show it being added.

        Then to make sure the chest cant be interacted with again and the item menu shows new items I:
        Set the chests animation to "Open" to show its open,
        Activate the "itemMenu" scripts gameobject to true (visible),
        Destroy the script so it can't be used again*/
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