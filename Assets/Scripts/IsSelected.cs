using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IsSelected : MonoBehaviour
{
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            this.GetComponentInChildren<TMPro.TMP_Text>().color = Color.yellow;
        }
        else
        {
            this.GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
        }
    }
}
