using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacons : MonoBehaviour
{
    public InputReader ir;
    public void ClsoeMenu()
    {
        ir.GoToUI();
        gameObject.SetActive(false);
    }
}
