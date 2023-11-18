using UnityEngine;

namespace ProjectTD
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Character Speed")]
        [SerializeField]
        private float _walkSpeed = 8f;

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;

        private Rigidbody _rigidbody;
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
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Movement();
            Animate();
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

        private void Animate()
        {
            if (_animator == null)
            {
                return;
            }

            Vector3 localMove = transform.InverseTransformDirection(_move);

            _animator.SetFloat("sideway", localMove.x);
            _animator.SetFloat("forward", localMove.z);
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
