using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    public List<string> keys = new List<string>();
    public List<QuickItem> quickItems = new List<QuickItem>();
    public ItemMenu itemMenu;

    public void OpenChest()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        foreach (string key in keys)
        {
            player.GetComponent<KeyInventory>().AddKey(key);
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