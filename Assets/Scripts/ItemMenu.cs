using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemMenu : MonoBehaviour
{
    public List<Item> newItems = new List<Item>();
    public List<QuickItem> newQuickItems = new List<QuickItem>();
    public List<string> items = new List<string>();

    private QuickItem curQuickItem;
    private Item curItem;

    private bool active;
    private float time;
    private float maxTime = 2.5f;

    private void Update()
    {
        if (active)
        {
            time += 1 * Time.deltaTime;

            if (time > maxTime)
            {
                active = false;
                time = 0;
                newItems.Remove(curItem);
                newQuickItems.Remove(curQuickItem);
            }
        }

        if (newItems.Count == 0 && newQuickItems.Count == 0 && !active)
        {
            gameObject.SetActive(false);
        }

        if (newItems.Count > 0)
        {
            NewItem(newItems[0]);
        }

        if (newQuickItems.Count > 0)
        {
            NewItem(newQuickItems[0]);
        }
    }

    public void AddToList(Item newItem)
    {
        newItems.Add(newItem);
    }
    public void AddToList(QuickItem newQuickItem)
    {
        newQuickItems.Add(newQuickItem);
    }

    public void NewItem(Item newItem)
    {
        if (active) return;
        curItem = newItem;
        active = true;
        transform.GetChild(3).GetComponent<Image>().sprite = newItem.Icon;
        transform.GetChild(1).GetComponent<TMP_Text>().text = newItem.ItemName;

        switch (newItem.itemType)
        {
            case Item.ItemType.key:
                transform.GetChild(1).GetComponent<TMP_Text>().text = string.Format("{0} Key", newItem.ItemName);
                break;
        }
    }

    public void NewItem(QuickItem newItem)
    {
        if (active) return;
        int id = newItem.ID;
        string text = CheckSmiliarQuickItems(newItem);
        active = true;
        transform.GetChild(3).GetComponent<Image>().sprite = newItem.Icon;
        transform.GetChild(1).GetComponent<TMP_Text>().text = text;
        newQuickItems.RemoveAll(QuickItem => QuickItem.ID == id);
    }

    private string CheckSmiliarQuickItems(QuickItem quickItem)
    {
        int num = 0;
        foreach (QuickItem _quickItem in newQuickItems)
        {
            if (_quickItem.ID == quickItem.ID)
            {
                num++;
            }
        }
        if (num != 0) return string.Format("{0} x{1}", quickItem.ItemName, num);
        return quickItem.ItemName;
    }
}
