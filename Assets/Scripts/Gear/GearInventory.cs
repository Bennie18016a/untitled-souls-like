using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GearInventory : MonoBehaviour
{
    public List<Gear> gear = new List<Gear>();
    public List<Gear> equippedGear = new List<Gear>();
    public List<GameObject> equippedUI = new List<GameObject>();

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Gear _gear in equippedGear)
        {
            switch (_gear.gearType)
            {
                case Gear.GearType.head:
                    equippedUI[0].transform.GetChild(0).GetComponent<Image>().sprite = _gear.Icon;
                    break;
                case Gear.GearType.chest:
                    equippedUI[1].transform.GetChild(0).GetComponent<Image>().sprite = _gear.Icon;
                    break;
                case Gear.GearType.legs:
                    equippedUI[2].transform.GetChild(0).GetComponent<Image>().sprite = _gear.Icon;
                    break;
                case Gear.GearType.feet:
                    equippedUI[3].transform.GetChild(0).GetComponent<Image>().sprite = _gear.Icon;
                    break;
                case Gear.GearType.weapon:
                    equippedUI[4].transform.GetChild(0).GetComponent<Image>().sprite = _gear.Icon;
                    break;
            }

            foreach (GameObject ui in equippedUI)
            {
                ui.transform.GetChild(0).gameObject.SetActive(ui.transform.GetChild(0).GetComponent<Image>().sprite != null);
            }
        }
    }
}
