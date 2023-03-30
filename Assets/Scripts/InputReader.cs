using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class InputReader : MonoBehaviour, Controls.IPlayerActions, Controls.IUIActions
{
    #region Public Values
    public Vector2 MovementValue { get; private set; }
    public Vector2 MouseValue { get; private set; }
    public Vector2 ScrollWheelValue { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsHeavyAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    #endregion

    #region UI Menus
    public GameObject Tabs;
    public GameObject PauseMenu;
    public GameObject InvMenu;
    #endregion

    #region Private Values
    private Controls _controls;
    private bool Targeting;
    private bool inUI;
    private bool TabsActive;
    #endregion

    #region Player Events
    public event Action CancelEvent;
    public event Action TargetEvent;
    public event Action DodgeEvent;
    public event Action QuickItemEvent;
    public event Action SwitchQuickItemEvent;
    public event Action InteractAction;
    #endregion

    #region UI Events
    //public event Action BackEvent;
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _controls = new Controls();
        _controls.Player.SetCallbacks(this);
        _controls.UI.SetCallbacks(this);

        _controls.Player.Enable();
    }

    public void GoToUI()
    {
        if (inUI)
        {
            inUI = false;
            _controls.Player.Enable();
            _controls.UI.Disable();
        }
        else
        {
            inUI = true;
            _controls.Player.Disable();
            _controls.UI.Enable();
        }
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
        _controls.UI.Disable();
    }

    #region Player

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseValue = context.ReadValue<Vector2>();
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        //if we are not targeting, target, else remove target
        if (!Targeting)
        {
            TargetEvent?.Invoke();
            Targeting = !Targeting;
        }
        else
        {
            CancelEvent?.Invoke();
            Targeting = !Targeting;
        }

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        IsAttacking = context.performed;
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        // if (context.performed) { IsBlocking = true; }
        // else if (context.canceled) { IsBlocking = false; }
        IsBlocking = context.performed;
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();

    }

    public void OnUseQuickItem(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        QuickItemEvent?.Invoke();
    }

    public void OnSwitchQuickItem(InputAction.CallbackContext context)
    {
        ScrollWheelValue = context.ReadValue<Vector2>();

        if (!context.performed) { return; }
        SwitchQuickItemEvent?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        InteractAction?.Invoke();
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        IsHeavyAttacking = context.performed;
    }

    public void OnPauseMenu(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TabsActive = true;
        Tabs.SetActive(TabsActive);
        GoToUI();
        EventSystem.current.SetSelectedGameObject(Tabs.transform.GetChild(1).gameObject);
    }
    #endregion

    #region UI
    public void OnNavigate(InputAction.CallbackContext context)
    {

    }

    public void OnSelect(InputAction.CallbackContext context)
    {

    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        if (PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(false);
            Tabs.SetActive(true);
            EventSystem.current.SetSelectedGameObject(Tabs.transform.GetChild(1).gameObject);
        }
        else if (InvMenu.activeInHierarchy)
        {
            InvMenu.SetActive(false);
            Tabs.SetActive(true);
            EventSystem.current.SetSelectedGameObject(Tabs.transform.GetChild(1).gameObject);
        }
        else if (TabsActive)
        {
            TabsActive = false;
            Tabs.SetActive(TabsActive);
            GoToUI();
        }
    }
    #endregion
}
