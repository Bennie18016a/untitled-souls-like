using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickHandler : MonoBehaviour
{
    [SerializeField] private GameObject Leg;

    public void EnableKick()
    {
        Leg.SetActive(true);
    }

    public void DisableKick()
    {
        Leg.SetActive(false);
    }
}