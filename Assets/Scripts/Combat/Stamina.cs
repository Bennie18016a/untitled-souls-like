using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    #region Variables
    public GameObject StaminaSlider;
    public float NaturalStaminaMaxTime;
    [SerializeField] private int maxStamina;
    [SerializeField] private int gearMaxStamina;
    private int _stamina;
    private float naturalStaminaTime;
    private bool canNaturalStamina;
    #endregion

    #region Unity Functions
    private void Start()
    {
        NewEquipment();
        _stamina = gearMaxStamina;
        canNaturalStamina = true;
    }

    private void Update()
    {
        StaminaSlider.GetComponent<Slider>().maxValue = gearMaxStamina;
        NaturalStamina();
        if (StaminaSlider.gameObject.activeInHierarchy) { UpdateSlider(); }
        naturalStaminaTime += 1 * Time.deltaTime;
    }

    public void NewEquipment()
    {
        GearInventory gi = GetComponent<GearInventory>();
        gearMaxStamina = maxStamina;

        foreach (Gear gear in gi.equippedGear)
        {
            gearMaxStamina += gear.stamina;
        }
    }
    #endregion

    #region Functions
    public void TakeStamina(int stamina)
    {
        _stamina = Mathf.Max(_stamina - stamina, 0);
    }

    public void AddStamina(int stamina)
    {
        _stamina = Mathf.Min(_stamina + stamina, gearMaxStamina);
    }

    public void SetMaxStamina(int newMaxStamina)
    {
        maxStamina = newMaxStamina;
    }

    public void SetNaturalStamina(bool canStamina)
    {
        canNaturalStamina = canStamina;
    }

    public bool CanAction(int toTake)
    {
        int newStamina = _stamina - toTake;
        return newStamina > 0;
    }

    private void NaturalStamina()
    {
        if (!canNaturalStamina) { return; }
        if (_stamina >= gearMaxStamina) { _stamina = gearMaxStamina; return; }
        if (naturalStaminaTime < NaturalStaminaMaxTime) { return; }

        _stamina = Mathf.Max(_stamina + 1, 0);
        naturalStaminaTime = 0;
    }

    private void UpdateSlider()
    {
        StaminaSlider.GetComponent<Slider>().value = _stamina;
    }
    #endregion
}
