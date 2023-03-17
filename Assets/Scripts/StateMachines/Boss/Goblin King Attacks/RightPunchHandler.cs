using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPunchHandler : MonoBehaviour
{
    [SerializeField] private GameObject RightHand;

    public void EnableRightHand()
    {
        RightHand.SetActive(true);
    }

    public void DisableRightHand()
    {
        RightHand.SetActive(false);
    }
}