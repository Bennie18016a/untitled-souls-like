using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPunchHandler : MonoBehaviour
{
    [SerializeField] private GameObject LeftHand;

    public void EnableLeftHand()
    {
        LeftHand.SetActive(true);
    }

    public void DisableLeftHand()
    {
        LeftHand.SetActive(false);
    }
}