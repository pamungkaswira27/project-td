using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGame : MonoBehaviour
{

    [SerializeField] private GameObject _pauseGamePanel;

    private PlayerInputAction _inputActions;
    private bool _isPaused;

    private void Awake()
    {
        _inputActions = new PlayerInputAction();
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
        PlayerManager.Instance.CharacterAim.enabled = false;
    }

    public void UIPauseGameDeactived()
    {
        Time.timeScale = 1;
        _pauseGamePanel.SetActive(false);
        InputManager.Instance.EnablePlayerInput();
        PlayerManager.Instance.CharacterAim.enabled = true;
    }
}
