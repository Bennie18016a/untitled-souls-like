using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SplashScreen : MonoBehaviour
{
    public void FinishSplashScreen()
    {
        transform.parent.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(GameObject.Find("Start Game"));
    }
}
