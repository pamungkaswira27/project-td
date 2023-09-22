using UnityEngine;

namespace ProjectTD
{
    public class InputManager : MonoBehaviour
    {
        private PlayerInputAction _playerInputAction;

        private CharacterPrimaryFire _characterPrimaryFire;
        private CharacterUltimateFire _characterUltimateFire;

        private Coroutine _fireCoroutine;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();

            _characterPrimaryFire = FindFirstObjectByType<CharacterPrimaryFire>();
            _characterUltimateFire = FindFirstObjectByType<CharacterUltimateFire>();
        }

        private void OnEnable()
        {
            _playerInputAction.Enable();

            SubscribeInputEvents();
        }

        private void OnDisable()
        {
            _playerInputAction.Disable();

            UnsubscribeInputEvents();
        }

        private void SubscribeInputEvents()
        {
            _playerInputAction.Player.Fire.started += _ => StartFire();
            _playerInputAction.Player.Fire.canceled += _ => StopFire();
            _playerInputAction.Player.Ultimate.performed += _ => ActivateUltimate();
        }

        private void UnsubscribeInputEvents()
        {
            _playerInputAction.Player.Fire.started -= _ => StartFire();
            _playerInputAction.Player.Fire.canceled -= _ => StopFire();
            _playerInputAction.Player.Ultimate.performed -= _ => ActivateUltimate();
        }

        private void StartFire()
        {
            if (_characterUltimateFire.IsUltimateActive())
            {
                _fireCoroutine = StartCoroutine(_characterUltimateFire.FireCoroutine());
            }
            else
            {
                _fireCoroutine = StartCoroutine(_characterPrimaryFire.FireCoroutine());
            }
        }

        private void StopFire()
        {
            if (_fireCoroutine != null && !_characterUltimateFire.IsUltimateActive())
            {
                StopCoroutine(_fireCoroutine);
            }
        }

        private void ActivateUltimate()
        {
            _characterUltimateFire.ActivateUltimate();
            StopFire();
        }
    }
}
