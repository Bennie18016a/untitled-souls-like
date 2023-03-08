using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Health PlayerHealth;
    private int _damage;
    public List<GameObject> AlreadyCollided = new List<GameObject>();

    private void OnEnable()
    {
        AlreadyCollided.Clear();
    }

    public void SetDamage(int newDamage)
    {
        this._damage = newDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider) { return; }
        if (AlreadyCollided.Contains(other.transform.parent.gameObject)) { return; }
        AlreadyCollided.Add(other.transform.parent.gameObject);

        if (other.transform.parent.TryGetComponent<Health>(out Health health))
        {
            if (health == PlayerHealth) return;
            health.DealDamage(_damage);
        }
    }
}