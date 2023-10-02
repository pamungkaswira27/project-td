using UnityEngine;

namespace ProjectTD
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Character Speed")]
        [SerializeField]
        private float _walkSpeed = 8f;

        private CapsuleCollider _capsuleCollider;
        private Vector2 _direction;
        private Vector3 _move;
        private Vector3 _positionCollider;
        private float _moveSpeed;
        private float _heightCollider;
        private bool _isRunning = false;
        private bool _isRolling = false;

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
            }
        }

        public bool IsRolling
        {
            get
            {
                return _isRolling;
            }
            set
            {
                _isRolling = value;
            }
        }

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider>();

        }

        private void FixedUpdate()
        {
            Movement();
        }

        private void Movement()
        {
            _moveSpeed = _walkSpeed;
            _direction = InputManager.Instance.GetMovementInputVector();

            if (_isRunning)
            {
                _moveSpeed = _walkSpeed * 2;
            }
            else if (_isRolling)
            {
                RollingMovement();
            }
            else if (!_isRolling)
            {
                RollCompleted();
            }

            _move = new(_direction.x, 0, _direction.y);

            transform.position += _moveSpeed * Time.deltaTime * _move;
        }

        private void RollingMovement()
        {
            _heightCollider = 0.5f;
            _positionCollider = new(0, 0.5f, 0);

            _capsuleCollider.height = _heightCollider;
            _capsuleCollider.center = _positionCollider;
        }

        private void RollCompleted()
        {
            _heightCollider = 2;
            _positionCollider = new(0, 1, 0);

            _capsuleCollider.height = _heightCollider;
            _capsuleCollider.center = _positionCollider;
        }
    }
}
