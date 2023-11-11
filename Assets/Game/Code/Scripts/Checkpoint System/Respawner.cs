using UnityEngine;

namespace ProjectTD
{
    public class Respawner : MonoBehaviour
    {
        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Start()
        {
            RespawnerManager.Instance.AddObjectToRespawn(this);

            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        public void Respawn()
        {
            transform.SetPositionAndRotation(_initialPosition, _initialRotation);

            if (TryGetComponent(out CharacterHealth characterHealth))
            {
                characterHealth.SetHealthPoints(characterHealth.MaxHealthPoints);
            }

            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
