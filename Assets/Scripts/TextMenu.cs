using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TextMenu : MonoBehaviour
{
    public TMP_Text text;
    public TMP_Text closeText;
    public InputReader ir;
    public PlayerStateMachine _stateMachine;

    private void OnEnable()
    {
        ir.InteractAction += CloseMenu;

        Controls _controls = new Controls();
        closeText.text = string.Format("{0}: Close Menu", _controls.Player.Interact.GetBindingDisplayString());
    }

    public void OpenMenu(string _text)
    {
        _stateMachine.CanMove = false;
        gameObject.SetActive(true);
        text.text = _text;
    }

    public bool InMenu(){
        return gameObject.activeInHierarchy;
    }

    private void CloseMenu()
    {
        _stateMachine.CanMove = true;
        text.text = null;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ir.InteractAction -= CloseMenu;
    }
}
