using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInputAction _playerInputAction;
    private CharacterPrimaryFire _characterPrimaryFire;

    private Coroutine _fireCoroutine;

    private void Awake()
    {
        _playerInputAction = new PlayerInputAction();
        _characterPrimaryFire = FindFirstObjectByType<CharacterPrimaryFire>();
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
    }

    private void UnsubscribeInputEvents()
    {
        _playerInputAction.Player.Fire.started -= _ => StartFire();
        _playerInputAction.Player.Fire.canceled -= _ => StopFire();
    }

    private void StartFire()
    {
        _fireCoroutine = StartCoroutine(_characterPrimaryFire.FireCoroutine());
    }

    private void StopFire()
    {
        if (_fireCoroutine != null)
        {
            StopCoroutine(_fireCoroutine);
        }
    }
}
