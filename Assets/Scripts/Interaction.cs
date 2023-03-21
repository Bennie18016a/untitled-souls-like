using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public enum Type { door, keydoor, onesidedoor, fogwall }
    public Type _type;
    public InputReader ir;
    private GameObject player;
    public float range = 3f;

    private void OnEnable()
    {
        ir.InteractAction += Interact;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        bool inRange = Vector3.Distance(transform.position, player.transform.position) > range;
        if (inRange) return;

        switch (_type)
        {
            case Type.door:
                Destroy(gameObject);
                break;
            case Type.keydoor:
                if (!GetComponent<CheckInteraction>().HasKey(transform.name)) return;
                Destroy(gameObject);
                break;
            case Type.onesidedoor:
                if (!GetComponentInChildren<CheckInteraction>().IsInArea()) return;
                Destroy(gameObject);
                break;
            case Type.fogwall:
                if (!transform.GetChild(1).GetComponent<CheckInteraction>().IsInArea()) return;
                Debug.Log("Here");
                player.transform.position = transform.GetChild(0).transform.position;
                GetComponent<FogWall>().boss.GetComponent<BossStateMachine>().Active = true;
                break;
        }
    }

    private void OnDisable()
    {
        ir.InteractAction -= Interact;
    }

    private void OnDestroy()
    {
        ir.InteractAction -= Interact;
    }
}
