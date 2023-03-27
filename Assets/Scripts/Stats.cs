using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats : MonoBehaviour, IDataPersistence
{
    [field: SerializeField] public int Health;
    [field: SerializeField] public int Magic;
    [field: SerializeField] public int Strength;
    [field: SerializeField] public int Dexterity;
    [field: SerializeField] public int Knowledge;

    public void LoadData(GameData data)
    {
        this.Health = data.Health;
        this.Magic = data.Magic;
        this.Strength = data.Strength;
        this.Dexterity = data.Dexterity;
        this.Knowledge = data.Knowledge;
    }

    public void SaveData(ref GameData data)
    {
        data.Health = this.Health;
        data.Magic = this.Magic; ;
        data.Strength = this.Strength;
        data.Dexterity = this.Dexterity;
        data.Knowledge = this.Knowledge;
    }
}