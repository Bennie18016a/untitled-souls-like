using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiBehaviour : MonoBehaviour
{
    Vector3 target;
    bool flying;
    public float speed;
    public int damage;

    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>().position;
    }

    private void Update()
    {
        target.y += 50;
        transform.LookAt(target);

        if (flying)
        {
            Vector3 targetPos = target;
            targetPos.y = transform.position.y;

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, targetPos);
            if (distance < .05)
            {
                Destroy(gameObject);
            }
        }
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        target = GameObject.Find("Player").GetComponent<Transform>().position;
        flying = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.GetComponent<Health>().DealDamage(damage, false);
        other.transform.GetComponent<ForceReciver>().AddForce((other.transform.position - transform.position).normalized * 20);
        Destroy(gameObject);
    }
}
