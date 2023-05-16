using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    #region Public Fields
    public GameObject HealthSlider;
    #endregion

    #region Private Fields
    [SerializeField] private int MaximumHealth = 100;
    [SerializeField] private int gearMaxHealth;
    private int _health;
    private bool _isDead => _health == 0;
    private bool isInvunerable;
    #endregion

    #region Events
    public event Action OnTakeDamage;
    public event Action OnDie;
    #endregion

    #region Unity Functions
    private void Start()
    {
        NewEquipment();
        _health = gearMaxHealth;
        if (gameObject.name == "Player") return;
        HealthSlider.SetActive(false);
    }

    private void Update()
    {
        HealthSlider.GetComponent<Slider>().maxValue = gearMaxHealth;
        if (HealthSlider.gameObject.activeInHierarchy) { UpdateSlider(); }
    }

    public void NewEquipment()
    {
        TryGetComponent<GearInventory>(out GearInventory gi);
        gearMaxHealth = MaximumHealth;
        if (gi == null) return;

        foreach (Gear gear in gi.equippedGear)
        {
            gearMaxHealth += gear.health;
        }
    }
    #endregion

    #region Functions
    public void Invunerable(bool set)
    {
        isInvunerable = set;
    }

    public void DealDamage(int damage, bool ignore)
    {
        if (isInvunerable && !ignore) return;
        _health = Mathf.Max(_health - damage, 0);

        OnTakeDamage?.Invoke();

        if (_isDead) { OnDie?.Invoke(); }
    }

    public void ToggleSlider(bool toggle)
    {
        HealthSlider.SetActive(toggle);
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        MaximumHealth = newMaxHealth;
    }

    public void AddHealth(int health)
    {
        _health = Mathf.Min(_health + health, gearMaxHealth);
    }

    private void UpdateSlider()
    {
        HealthSlider.GetComponent<Slider>().value = _health;
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetMaxHealth()
    {
        return MaximumHealth;
    }
    #endregion
}