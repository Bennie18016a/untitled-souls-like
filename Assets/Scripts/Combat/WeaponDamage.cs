using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Health PlayerHealth;
    private int _damage;
    private float _knockback;
    public List<Collider> AlreadyCollided = new List<Collider>();

    private void OnEnable()
    {
        AlreadyCollided.Clear();
    }

    public void SetDamage(int newDamage, float Knockback)
    {
        this._damage = newDamage;
        this._knockback = Knockback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider) { return; }
        if (AlreadyCollided.Contains(other)) { return; }
        AlreadyCollided.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            if (health == PlayerHealth) return;
            health.DealDamage(_damage);
        }

        if (other.TryGetComponent<ForceReciver>(out ForceReciver forceReciver) && _knockback > 0)
        {
            forceReciver.AddForce((other.transform.position - playerCollider.transform.position).normalized * _knockback);
        }
    }
}