using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterAim : MonoBehaviour
{
    [SerializeField]
    private LayerMask _groundLayer;

    private Camera _camera;
    private Ray _ray;
    private RaycastHit _raycastHit;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        _mousePosition = Mouse.current.position.ReadValue();
        _ray = _camera.ScreenPointToRay(_mousePosition);

        if (Physics.Raycast(_ray, out _raycastHit, Mathf.Infinity, _groundLayer))
        {
            Vector3 aimDirection = _raycastHit.point - transform.position;
            transform.localRotation = Quaternion.LookRotation(aimDirection);
        }
    }
}
