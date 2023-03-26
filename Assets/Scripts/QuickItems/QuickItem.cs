using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quick Item", fileName = "New Quick Item")]
public class QuickItem : ScriptableObject
{
    public string ItemName;
    public int ID;
    public int Number;
    public int DefaultNumber;
    public bool Active;
    public Sprite Icon;
}
