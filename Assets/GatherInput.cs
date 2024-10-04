using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    private Controls myControl;
    public float valueX;
    public bool jumpInput;
    public bool tryAttack = false;
        
    public void Awake()
    {
        myControl = new Controls();
    }
    private void OnEnable()
    {
        myControl.Player.Move.performed += StartMove;
        myControl.Player.Move.canceled += StopMove;

        myControl.Player.jump.performed += JumpStart;
        myControl.Player.jump.canceled += JumpStop;

        myControl.Player.Attack.performed += TryToAttach;
        myControl.Player.Attack.canceled += StopTryToAttack;

        myControl.Player.Enable();
    }

    private void OnDisable()
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;

        myControl.Player.jump.performed -= JumpStart;
        myControl.Player.jump.canceled -= JumpStop;

        myControl.Player.Attack.performed -= TryToAttach;
        myControl.Player.Attack.canceled -= StopTryToAttack;
        myControl.Player.Disable();
        //myControl.Disable();
    }
    public void DisableControls()
    {
        myControl.Player.Move.performed -= StartMove;
        myControl.Player.Move.canceled -= StopMove;

        myControl.Player.jump.performed -= JumpStart;
        myControl.Player.jump.canceled -= JumpStop;

        myControl.Player.Attack.performed -= TryToAttach;
        myControl.Player.Attack.canceled -= StopTryToAttack;

        myControl.Player.Disable();
        valueX = 0;
    }
    private void StartMove(InputAction.CallbackContext ctx)
    {
        valueX = ctx.ReadValue<float>();
    }
    private void StopMove(InputAction.CallbackContext ctx)
    {
        valueX = 0;
    }
    private void JumpStart(InputAction.CallbackContext ctx)
    {
        jumpInput = true;
    }
    private void JumpStop(InputAction.CallbackContext ctx)
    {
        jumpInput = false;
    }
    private void TryToAttach(InputAction.CallbackContext ctx)
    {
        tryAttack = true;
    }
    private void StopTryToAttack(InputAction.CallbackContext ctx)
    {
        tryAttack = false;
    }
}