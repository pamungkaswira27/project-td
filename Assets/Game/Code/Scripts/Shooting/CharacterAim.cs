using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectTD
{
    public class CharacterAim : MonoBehaviour
    {

        private Camera _camera;
        private Ray _ray;
        private Vector3 _mousePosition;
        private Plane _plane;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _plane = new Plane(Vector3.up, Vector3.zero);
        }

        private void Update()
        {
            _mousePosition = Mouse.current.position.ReadValue();
            _ray = _camera.ScreenPointToRay(_mousePosition);

            if (_plane.Raycast(_ray, out float enter))
            {
                Vector3 aimDirection = _ray.GetPoint(enter) - transform.position;
                transform.localRotation = Quaternion.LookRotation(aimDirection);
            }
        }
    }
}
