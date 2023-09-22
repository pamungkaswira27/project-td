using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CapsuleCollider _capsCollider;
    private Rigidbody _rb;
    private Vector2 _direction;
    private Vector3 _move;
    private Vector3 _postCollider;
    private float _moveSpeed;
    private float _heighCollider;

    [SerializeField]
    private MovementControls _controls;

    [SerializeField]
    private GameObject _playerObject;

    [Header("Speed")]
    [SerializeField]
    private float _walkSpeed = 8f;
    [SerializeField]
    private float _rotateSpeed = 12f;

    [Header("Condition")]
    public bool _running = false;
    public bool _rolling = false;

    private void Awake()
    {
        _capsCollider = GetComponent<CapsuleCollider>();
        _rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        _moveSpeed = _walkSpeed;
        _direction = _controls.InputWalkMovement();
        
        if (_running)
        {
            _moveSpeed = _walkSpeed * 2;
        }
        else if (_rolling)
        {
            RollingMovement();
        }
        else if (!_rolling)
        {
            RollCompleted();
        }

        _move = new(_direction.x, 0, _direction.y);

        transform.position += _moveSpeed * Time.deltaTime * _move;
        transform.forward = Vector3.Slerp(transform.forward, _move, _rotateSpeed * Time.deltaTime);

        Debug.Log(_moveSpeed);
    }

    private void RollingMovement()
    {
        _heighCollider = 0.5f;
        _postCollider = new(0, 0.5f, 0);

        _capsCollider.height = _heighCollider;
        _capsCollider.center = _postCollider;

    }

    private void RollCompleted()
    {
        _heighCollider = 2;
        _postCollider = new(0, 1, 0);

        _capsCollider.height = _heighCollider;
        _capsCollider.center = _postCollider;
        
    }
}
