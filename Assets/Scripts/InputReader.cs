using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    #region Public Values
    public Vector2 MovementValue { get; private set; }
    public Vector2 MouseValue { get; private set; }
    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    #endregion

    #region Private Values
    private Controls _controls;
    private bool Targeting;
    #endregion

    #region Events
    public event Action CancelEvent;
    public event Action TargetEvent;
    public event Action DodgeEvent;
    public event Action QuickItemEvent;
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _controls = new Controls();
        _controls.Player.SetCallbacks(this);

        _controls.Player.Enable();
    }

    private void OnDestroy()
    {
        _controls.Player.Disable();
    }

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
}
