using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider playerCollider;
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
        if(other.transform.parent == playerCollider.gameObject) return;
        if (AlreadyCollided.Contains(other.transform.gameObject)) { return; }
        Debug.Log(other.transform.name);
        AlreadyCollided.Add(other.transform.gameObject);

        if (other.transform.parent.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(_damage);
        }
    }
}