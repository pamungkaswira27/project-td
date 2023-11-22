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
        private Vector2 _direction;
        private Vector3 _movement;
        private float _moveSpeed;
        private bool _isRunning = false;

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
            Movement();
            Animate();
        }

        private void Movement()
        {
            _direction = InputManager.Instance.GetMovementInputVector();
            _moveSpeed = _walkSpeed;

            _movement = new(_direction.x, 0, _direction.y);
            transform.position += _moveSpeed * Time.deltaTime * _movement;
        }

        private void Animate()
        {
            if (_animator == null)
            {
                return;
            }

            Vector3 localMove = transform.InverseTransformDirection(_movement);

            _animator.SetFloat("sideway", localMove.x);
            _animator.SetFloat("forward", localMove.z);
        }

        public void Rolling()
        {
            if (_movement == Vector3.zero)
            {
                _rigidbody.AddRelativeForce(Vector3.forward * _rollForce);
                return;
            }

            _rigidbody.AddForce(_movement * _rollForce);
        }
    }
}
