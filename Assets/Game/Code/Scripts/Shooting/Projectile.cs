using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _damage;
        [SerializeField]
        private ProjectileType _type;
        [ShowIf("_type", ProjectileType.Ultimate)]
        [SerializeField]
        private int _maxHitCount;

        [Space]

        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _maxBulletTravelTime;

        private Vector3 _shootDirection;
        private int _hitCount;

        private enum ProjectileType
        {
            Normal,
            Ultimate
        }

        private void OnEnable()
        {
            _hitCount = 0;
            Invoke(nameof(DeactivateProjectile), _maxBulletTravelTime);
        }

        private void Update()
        {
            transform.position += _shootDirection * Time.deltaTime * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_type == ProjectileType.Ultimate)
            {
                _hitCount++;

                if (_hitCount >= _maxHitCount)
                {
                    DeactivateProjectile();
                }
            }
        }

        public void SetShootDirection(Vector3 shootDirection)
        {
            _shootDirection = shootDirection;
        }

        private void DeactivateProjectile()
        {
            gameObject.SetActive(false);
        }
    }
}
