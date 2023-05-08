using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gear", fileName = "New Gear")]
public class Gear : ScriptableObject
{
    public string GearName;
    public Sprite Icon;
    public enum GearType { weapon, head, chest, legs, feet }
    public GearType gearType;

    [Header("Weapon")]
    public int damage;
    public int heavyDamage;
    public enum WeaponType { swordNshield }
    public WeaponType weaponType;

    [Header("Armour")]
    public int health;
    public int stamina;
}
