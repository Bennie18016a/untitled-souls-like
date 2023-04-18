using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealBehaviour : MonoBehaviour
{
    Vector3 target;
    public float speed;
    public float timeTillDestroy;
    float time;

    private void Update()
    {
        target = GameObject.Find("Player").transform.position;
        target.y += 50;
        transform.LookAt(target);

        Vector3 targetPos = target;
        targetPos.y = transform.position.y;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPos);

        if (time > timeTillDestroy)
        {
            Destroy(gameObject);
        }

        time += 1 * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        other.transform.GetComponent<LifeStealTrap>().m_IsTrapped = true;
        other.transform.GetComponent<LifeStealTrap>().enabled = true;
        Destroy(gameObject);
    }
}
