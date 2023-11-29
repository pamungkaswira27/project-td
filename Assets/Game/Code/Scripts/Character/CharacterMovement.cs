using StinkySteak.SimulationTimer;
using UnityEngine;

namespace ProjectTD
{
    public class CharacterMovement : MonoBehaviour
    {
        [Header("Character Speed")]
        [SerializeField]
        private float _walkSpeed = 8f;
        [SerializeField]
        private float _runSpeed = 16f;

        [Header("Character Roll")]
        [SerializeField]
        private float _rollForce = 550f;

        [Header("Animation")]
        [SerializeField]
        private Animator _animator;

        private Rigidbody _rigidbody;
        private SimulationTimer _rollTimer;
        private Vector2 _direction;
        private Vector3 _movement;
        private float _moveSpeed;
        private bool _isRunning;
        private bool _isRolling;
        private float _rollDuration = 1.5f;

        public bool IsRolling => _isRolling;
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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (_rollTimer.IsExpired())
            {
                PlayerManager.Instance.CharacterAim.enabled = true;
                _isRolling = false;
            }

            Movement();
            Animate();
        }

        private void Movement()
        {
            if (_isRolling)
            {
                return;
            }

            _direction = InputManager.Instance.GetMovementInputVector();
            _moveSpeed = _walkSpeed;

            if (_isRunning)
            {
                _moveSpeed = _runSpeed;
            }

            _movement = new Vector3(_direction.x, 0, _direction.y);
            transform.position += _moveSpeed * Time.deltaTime * _movement;
        }

        private void Animate()
        {
            if (_animator == null)
            {
                return;
            }

            Vector3 localMove = transform.InverseTransformDirection(_movement);

            _animator.SetBool("isWalking", localMove.magnitude > 0);
            _animator.SetFloat("sideway", localMove.x);
            _animator.SetFloat("forward", localMove.z);
        }

        public void Rolling()
        {
            // Lock Player Direction and Movement
            _rollTimer = SimulationTimer.CreateFromSeconds(_rollDuration);
            PlayerManager.Instance.CharacterAim.enabled = false;
            _isRolling = true;

            // Play Roll Animation
            _animator.SetTrigger("rolling");

            if (_movement == Vector3.zero)
            {
                _rigidbody.AddRelativeForce(Vector3.forward * _rollForce);
                return;
            }

            transform.localRotation = Quaternion.LookRotation(_movement);
            _rigidbody.AddForce(_movement * _rollForce);
        }
    }
}
