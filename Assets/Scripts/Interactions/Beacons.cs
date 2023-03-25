using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacons : MonoBehaviour
{
    public InputReader ir;
    public void CloseMenu()
    {
        ir.GoToUI();
        gameObject.SetActive(false);
    }
}
