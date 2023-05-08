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
    private List<Gear> shownGear = new List<Gear>();
    public List<Image> slots = new List<Image>();
    public GameObject gearUI;
    public GameObject content;
    public GameObject _default;

    private void Update()
    {
        UpdateUI();
        UpdateSelected();
    }

    public int GetDamage(bool heavy)
    {
        foreach (Gear gear in equippedGear)
        {
            if (gear.gearType != Gear.GearType.weapon)
                continue;

            if (heavy)
                return gear.heavyDamage;
            else
                return gear.damage;
        }

        Debug.Log("No Weapon Found");
        return 0;
    }

    public void StartSwap(Transform button)
    {
        switch (button.name.ToLower())
        {
            case "head":
                Swap(Gear.GearType.head);
                break;
            case "chest":
                Swap(Gear.GearType.chest);
                break;
            case "legs":
                Swap(Gear.GearType.legs);
                break;
            case "feet":
                Swap(Gear.GearType.feet);
                break;
            case "weapon":
                Swap(Gear.GearType.weapon);
                break;
            default:
                Debug.Log("Error finding: " + button.name.ToLower());
                break;
        }
    }

    private void Swap(Gear.GearType type)
    {
        if (content.transform.childCount != 0)
        {
            foreach (Transform child in content.GetComponentInChildren<Transform>())
            {
                Debug.Log(child.name);
                if (child == content) continue;
                Destroy(child.gameObject);
            }
        }

        shownGear.Clear();

        foreach (Gear _gear in gear)
        {
            if (_gear.gearType == type)
            {
                shownGear.Add(_gear);
            }
        }

        foreach (Gear _gear in shownGear)
        {
            GameObject ui = GameObject.Instantiate(gearUI, Vector3.zero, Quaternion.identity);
            ui.transform.GetChild(1).GetComponent<Image>().sprite = _gear.Icon; ;
            ui.transform.GetChild(0).GetComponent<TMP_Text>().text = _gear.GearName;
            if (equippedGear.Find(x => x == _gear)) ui.transform.GetChild(0).GetComponent<TMP_Text>().text = string.Format("{0} - Equipped", _gear.GearName);
            ui.transform.parent = content.transform;

            Button button = ui.GetComponent<Button>();
            button.onClick.AddListener(() => SwapGear(_gear));
        }
    }

    private void SwapGear(Gear newgear)
    {
        foreach (Gear currentlyEquipped in equippedGear)
        {
            if (currentlyEquipped.gearType != newgear.gearType) continue;

            equippedGear.Remove(currentlyEquipped);
            equippedGear.Add(newgear);
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_default);
            Swap(newgear.gearType);
        }
    }

    private void UpdateSelected()
    {
        foreach (Image slot in slots)
        {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == slot.gameObject)
                slot.color = new Color(1, .92f, .16f, .75f);
            else
                slot.color = new Color(0, 0, 0, .75f);
        }
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
