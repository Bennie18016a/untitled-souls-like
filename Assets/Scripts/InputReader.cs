using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    private Controls _controls;
    private bool Targeting;

    public bool IsAttacking;

    #region Events
    public event Action CancelEvent;
    public event Action TargetEvent;
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

    public void OnAttack(InputAction.CallbackContext context){
        IsAttacking = context.performed;
    }
}
