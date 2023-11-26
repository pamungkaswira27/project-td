using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{

    [SerializeField] private GameObject _pauseGamePanel;

    private PlayerInputAction _inputActions;
    private CharacterAim _aim;
    private bool _isPaused;

    private void Awake()
    {
        _inputActions = new PlayerInputAction();
    }

    private void Start()
    {
        _aim = CharacterAim.Instance;
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.UI.PauseGame.performed += GamePause;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void GamePause(InputAction.CallbackContext context)
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            UIPauseGameActive();
        }
        else
        {
            UIPauseGameDeactived();
        }
    }

    private void UIPauseGameActive()
    {
        Time.timeScale = 0;
        _pauseGamePanel.SetActive(true);
        InputManager.Instance.DisablePlayerInput();
        _aim.enabled = false;
    }

    public void UIPauseGameDeactived()
    {
        Time.timeScale = 1;
        _pauseGamePanel.SetActive(false);
        InputManager.Instance.EnablePlayerInput();
        _aim.enabled = true;
    }
}
