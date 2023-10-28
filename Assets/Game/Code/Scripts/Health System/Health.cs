using UnityEngine;

namespace ProjectTD
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float _healthPoints;
        [SerializeField]
        private GameObject _itemDropPrefab;

        private Vector3 _positionDropItem;

        public void TakeDamage(float damagePoints)
        {
            _healthPoints -= damagePoints;

            if (_healthPoints <= 0f)
            {
                _positionDropItem = new Vector3(transform.position.x, 1, transform.position.z);
                _healthPoints = 0f;
                Destroy(gameObject);

                Instantiate(_itemDropPrefab, _positionDropItem, Quaternion.identity);

                if (!TryGetComponent<BaseCharacter>(out _)) return;

                PlayerManager.Instance.DecreaseLife();
            }

            if (_healthPoints <= 0f)
            {
                _healthPoints = 0f;
                gameObject.SetActive(false);
            }

        }
    }
}
