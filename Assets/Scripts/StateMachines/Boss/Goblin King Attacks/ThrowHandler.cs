using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHandler : MonoBehaviour
{
    [SerializeField] private GameObject GrabArea;

    public void EnableGrab()
    {
        GrabArea.SetActive(true);
    }

    public void DisableGrab()
    {
        GrabArea.SetActive(false);
    }
}
