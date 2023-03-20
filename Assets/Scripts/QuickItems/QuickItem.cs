using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "New Item")]
public class QuickItem : ScriptableObject
{
    public string ItemName;
    public int ID;
    public int Number;
    public int DefaultNumber;
    public bool Active;
}
