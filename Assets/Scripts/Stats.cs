using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public class Stats : MonoBehaviour, IDataPersistence
{
    [field: SerializeField] public int Health;
    [field: SerializeField] public int Magic;
    [field: SerializeField] public int Strength;
    [field: SerializeField] public int Dexterity;
    [field: SerializeField] public int Knowledge;

    public TMP_Text health, magic, strength, dexterity, knowledge;

    private void Update()
    {
        health.text = Health.ToString();
        magic.text = Magic.ToString();
        strength.text = Strength.ToString();
        dexterity.text = Dexterity.ToString();
        knowledge.text = Knowledge.ToString();
    }

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