using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadDamageHandler : MonoBehaviour
{
    public float WhenToActivateCollider;
    public Collider _collider;
    float time;
    bool activated;

    private void Update()
    {
        if (time > WhenToActivateCollider && !activated)
        {
            _collider.enabled = true;
            activated = true;
        }

        time += 1 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.GetComponent<ForceReciver>().Throw(Vector3.up, 15);
        other.GetComponent<Health>().DealDamage(75, true);
        _collider.enabled = false;
    }
}
