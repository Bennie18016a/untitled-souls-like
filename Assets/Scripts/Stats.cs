using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats{
    [field: SerializeField] public int Health;
    [field: SerializeField] public int Magic;
    [field: SerializeField] public int Strength;
    [field: SerializeField] public int Dexterity;
    [field: SerializeField] public int Knowledge;
}