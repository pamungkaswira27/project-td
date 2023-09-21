using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movements : MonoBehaviour
{
    private Projecttd _input;
    private Rigidbody _rb;
    private Vector2 _direction;
    private Vector3 _move;
    private float _moveSpeed;

    [Header("Speed")]
    [SerializeField] private float _walkSpeed = 8f;
    [SerializeField] private float _rotateSpeed = 12f;

    [Header("Condition")]
    [SerializeField] private bool _running;
    [SerializeField] private bool _runningKey;


    private void Awake()
    {
        _input = new Projecttd();
        _rb = GetComponent<Rigidbody>();

    }

    public void OnEnable()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMovement;
        _input.Player.Running.performed += OnSpecialMove;
        _input.Player.Rolling.performed += OnSpecialMove;


        _input.Player.Move.canceled += OnMovement;
        _input.Player.Running.canceled += OnSpecialMove;
        _input.Player.Rolling.canceled += OnSpecialMove;
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Player.Move.performed -= OnMovement;
        _input.Player.Running.performed -= OnSpecialMove;


        _input.Player.Move.canceled -= OnMovement;
        _input.Player.Running.canceled -= OnSpecialMove;
    }

   
    private void FixedUpdate()
    {
        WalkMovement();

        if (_runningKey)
        {
            _running = true;
        }
        else if (!_runningKey)
        {
            _running = false;
        }
    }

    private void WalkMovement()
    {
        _move = new(_direction.x, 0, _direction.y);
        _move.Normalize();

        if (_running)
        {
            _moveSpeed = _walkSpeed * 2;
        }
        else
        {
            _moveSpeed = _walkSpeed;
        }

        transform.position += _moveSpeed * Time.deltaTime * _move;
        transform.forward = Vector3.Slerp(transform.forward, _move, Time.deltaTime * _rotateSpeed);

    }

    private void OnMovement(InputAction.CallbackContext context)
    {
        _direction = context.ReadValue<Vector2>();
    }

    private void OnSpecialMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _runningKey = true;
        }
        else if (context.canceled)
        {
            _runningKey = false;
        }
    }

}
