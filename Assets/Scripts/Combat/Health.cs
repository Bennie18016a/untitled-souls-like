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
        _health = MaximumHealth;

        if (gameObject.name == "Player") return;
        HealthSlider.SetActive(false);
    }

    private void Update()
    {
        HealthSlider.GetComponent<Slider>().maxValue = MaximumHealth;
        if (HealthSlider.gameObject.activeInHierarchy) { UpdateSlider(); }
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
        _health = Mathf.Min(_health + health, MaximumHealth);
    }

    private void UpdateSlider()
    {
        HealthSlider.GetComponent<Slider>().value = _health;
    }
    #endregion
}