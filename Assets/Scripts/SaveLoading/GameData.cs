using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    #region Stats
    public int Health;
    public int Magic;
    public int Strength;
    public int Dexterity;
    public int Knowledge;
    #endregion
    #region Player Stats
    public Vector3 playerPosition;
    public SerializableDictionary<string, bool> beacons;
    public SerializableDictionary<string, int> QuickItems;
    public SerializableDictionary<string, string> Keys;
    #endregion

    #region Enemies
    public SerializableDictionary<string, bool> enemies;
    public SerializableDictionary<string, bool> bosses;
    #endregion
    #region Misc
    public SerializableDictionary<string, bool> interactions;
    #endregion
    public GameData()
    {
        this.Health = 1;
        this.Magic = 1;
        this.Strength = 1;
        this.Dexterity = 1;
        this.Knowledge = 1;

        playerPosition = new Vector3(23.672699f, -6.45960855f, -50.9613838f);
        beacons = new SerializableDictionary<string, bool>();
        enemies = new SerializableDictionary<string, bool>();
        bosses = new SerializableDictionary<string, bool>();
        QuickItems = new SerializableDictionary<string, int>();
        Keys = new SerializableDictionary<string, string>();
        interactions = new SerializableDictionary<string, bool>();
    }
}
