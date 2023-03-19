using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
    [SerializeField] private Health PlayerHealth;
    private int _damage;
    private float _knockback;
    private Vector3 _direction;
    private float _force;
    public bool AlwaysDealDamage;
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

    public void Force(Vector3 direction, float force)
    {
        this._direction = direction;
        this._force = force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider) { return; }
        if (AlreadyCollided.Contains(other)) { return; }
        AlreadyCollided.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            if (health == PlayerHealth) return;
            health.DealDamage(_damage, AlwaysDealDamage);
            Debug.Log("Hit Player");
        }

        if (other.TryGetComponent<ForceReciver>(out ForceReciver forceReciver) && _knockback > 0)
        {
            Debug.Log("Knockback");
            forceReciver.AddForce((other.transform.position - playerCollider.transform.position).normalized * _knockback);
        }

        if (other.TryGetComponent<ForceReciver>(out ForceReciver _forceReciver) && _force > 0)
        {
            _forceReciver.Throw(_direction, _force);
        }
    }
}