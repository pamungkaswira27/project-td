using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementControls : MonoBehaviour
{
    private PlayerInputAction _input;
    private Vector2 _inputVector;

    [SerializeField]
    Player _player;

    private void Awake()
    {
        _input = new PlayerInputAction();
    }

    private void OnEnable()
    {
        _input.Enable();

        PerfromedInput();
    }

    private void OnDisable()
    {
        _input.Disable();

        CanceledInput();
    }

    public Vector2 InputWalkMovement()
    {
        _inputVector = _input.Player.Move.ReadValue<Vector2>();
        _inputVector.Normalize();

        return _inputVector;
    }

    private void PerfromedInput()
    {
        _input.Player.Running.performed += RunMovementState;
        _input.Player.Rolling.performed += RollMovementState;

        _input.Player.Running.canceled += RunMovementState;
        _input.Player.Rolling.canceled += RollMovementState;
    }
    private void CanceledInput()
    {
        _input.Player.Running.performed -= RunMovementState;
        _input.Player.Rolling.performed -= RollMovementState;

        _input.Player.Running.canceled -= RunMovementState;
        _input.Player.Rolling.canceled -= RollMovementState;

    }

    private void RunMovementState(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player._running = true;
        }
        else if (context.canceled)
        {
            _player._running = false;
        }
    }

    private void RollMovementState(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _player._rolling = true;
        }
        else if (context.canceled)
        {
            _player._rolling = false;
            StartCoroutine(DelayedRoll(5));
        }
    }

    public IEnumerator DelayedRoll(float time)
    {
        _input.Player.Rolling.Disable();
        yield return new WaitForSeconds(time);

        _input.Player.Rolling.Enable();
    }
}
