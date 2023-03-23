using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void FinishSplashScreen()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
