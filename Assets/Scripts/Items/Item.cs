using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class Item : ScriptableObject
{
    public string ItemName;
    public int Number;
    public Sprite Icon;
    public enum ItemType { key }
    public ItemType itemType;
}
