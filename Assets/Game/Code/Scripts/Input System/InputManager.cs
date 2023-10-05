using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTD
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private PlayerInputAction _playerInputAction;

        private CharacterMovement _characterMovement;
        private CharacterBasicShoot _characterBasicShoot;
        private CharacterUltimateShoot _characterUltimateShoot;

        private Coroutine _fireCoroutine;
        private Vector2 _movementInputVector;
        private WaitForSeconds _delayedRollWaitForSeconds;

        private void Awake()
        {
            Instance = this;
            _playerInputAction = new PlayerInputAction();

            _characterMovement = FindFirstObjectByType<CharacterMovement>();
            _characterBasicShoot = FindFirstObjectByType<CharacterBasicShoot>();
            _characterUltimateShoot = FindFirstObjectByType<CharacterUltimateShoot>();
        }

        private void OnEnable()
        {
            _playerInputAction.Enable();

            SubscribeInputEvents();
        }

        private void Start()
        {
            _delayedRollWaitForSeconds = new WaitForSeconds(5f);
        }

        private void OnDisable()
        {
            _playerInputAction.Disable();

            UnsubscribeInputEvents();
        }

        private void SubscribeInputEvents()
        {
            // Character movement input
            _playerInputAction.Player.Running.performed += RunMovementState;
            _playerInputAction.Player.Rolling.performed += RollMovementState;

            _playerInputAction.Player.Running.canceled += RunMovementState;
            _playerInputAction.Player.Rolling.canceled += RollMovementState;

            // Character primary fire
            _playerInputAction.Player.Fire.started += _ => StartFire();
            _playerInputAction.Player.Fire.canceled += _ => StopFire();

            // Character ultimate fire
            _playerInputAction.Player.Ultimate.performed += _ => ActivateUltimate();
        }

        private void UnsubscribeInputEvents()
        {
            // Character movement
            _playerInputAction.Player.Running.performed -= RunMovementState;
            _playerInputAction.Player.Rolling.performed -= RollMovementState;

            _playerInputAction.Player.Running.canceled -= RunMovementState;
            _playerInputAction.Player.Rolling.canceled -= RollMovementState;

            // Character primary fire
            _playerInputAction.Player.Fire.started -= _ => StartFire();
            _playerInputAction.Player.Fire.canceled -= _ => StopFire();

            // Character ultimate fire
            _playerInputAction.Player.Ultimate.performed -= _ => ActivateUltimate();
        }

        public Vector2 GetMovementInputVector()
        {
            _movementInputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();
            _movementInputVector.Normalize();

            return _movementInputVector;
        }

        private void RunMovementState(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _characterMovement.IsRunning = true;
            }
            else if (context.canceled)
            {
                _characterMovement.IsRunning = false;
            }
        }

        private void RollMovementState(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _characterMovement.IsRolling = true;
            }
            else if (context.canceled)
            {
                _characterMovement.IsRolling = false;
                StartCoroutine(DelayedRollCoroutine());
            }
        }

        private IEnumerator DelayedRollCoroutine()
        {
            _playerInputAction.Player.Rolling.Disable();
            yield return _delayedRollWaitForSeconds;
            _playerInputAction.Player.Rolling.Enable();
        }

        private void StartFire()
        {
            if (_characterUltimateShoot.IsUltimateActive())
            {
                _fireCoroutine = StartCoroutine(_characterUltimateShoot.FireCoroutine());
            }
            else
            {
                _fireCoroutine = StartCoroutine(_characterBasicShoot.FireCoroutine());
            }
        }

        private void StopFire()
        {
            if (_fireCoroutine != null && !_characterUltimateShoot.IsUltimateActive())
            {
                StopCoroutine(_fireCoroutine);
            }
        }

        private void ActivateUltimate()
        {
            _characterUltimateShoot.ActivateUltimate();
            StopFire();
        }
    }
}
