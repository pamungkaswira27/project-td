using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTD
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private PlayerInputAction _playerInputAction;

        private PlayerManager _playerManager;

        private Coroutine _fireCoroutine;
        private Vector2 _movementInputVector;
        private WaitForSeconds _delayedRollWaitForSeconds;

        private void Awake()
        {
            Instance = this;
            _playerInputAction = new PlayerInputAction();
        }

        private void OnEnable()
        {
            _playerInputAction.Enable();

            SubscribeInputEvents();
        }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            _delayedRollWaitForSeconds = new WaitForSeconds(5f);
        }

        private void OnDisable()
        {
            _playerInputAction.Disable();

            UnsubscribeInputEvents();
        }

        public void EnablePlayerInput()
        {
            _playerInputAction.Enable();
        }

        public void DisablePlayerInput()
        {
            _playerInputAction.Disable();
        }

        private void SubscribeInputEvents()
        {
            // Character movement input
            _playerInputAction.Player.Running.performed += RunMovementState;
            //_playerInputAction.Player.Rolling.performed += RollMovementState;

            _playerInputAction.Player.Running.canceled += RunMovementState;

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
            //_playerInputAction.Player.Rolling.performed -= RollMovementState;

            _playerInputAction.Player.Running.canceled -= RunMovementState;

            // Character primary fire
            _playerInputAction.Player.Fire.started -= _ => StartFire();
            _playerInputAction.Player.Fire.canceled -= _ => StopFire();

            // Character ultimate fire
            _playerInputAction.Player.Ultimate.performed -= _ => ActivateUltimate();
        }

        public Vector2 GetMovementInputVector()
        {
            _movementInputVector = _playerInputAction.Player.Move.ReadValue<Vector2>();
            //_movementInputVector.Normalize();

            return _movementInputVector;
        }

        private void RunMovementState(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerManager.CharacterMovement.IsRunning = true;
            }
            else if (context.canceled)
            {
                _playerManager.CharacterMovement.IsRunning = false;
            }
        }

        private void RollMovementState(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                StopFire();
                _playerManager.CharacterMovement.Rolling();
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
            if (_playerManager.CharacterMovement.IsRolling)
            {
                return;
            }

            if (_playerManager.CharacterUltimateShoot.IsUltimateActive())
            {
                _playerManager.CharacterUltimateShoot.PlayMuzzleFlashVFX();
                _fireCoroutine = StartCoroutine(_playerManager.CharacterUltimateShoot.FireCoroutine());
            }
            else
            {
                _playerManager.CharacterBasicShoot.PlayMuzzleFlashVFX();
                _fireCoroutine = StartCoroutine(_playerManager.CharacterBasicShoot.FireCoroutine());
            }
        }

        private void StopFire()
        {
            if (_fireCoroutine != null && !_playerManager.CharacterUltimateShoot.IsUltimateActive())
            {
                _playerManager.CharacterBasicShoot.StopMuzzleFlashVFX();
                StopCoroutine(_fireCoroutine);
            }
        }

        private void ActivateUltimate()
        {
            if (_playerManager.CharacterMovement.IsRolling)
            {
                return;
            }

            StopFire();
            _playerManager.CharacterUltimateShoot.ActivateUltimate();
        }
    }
}
