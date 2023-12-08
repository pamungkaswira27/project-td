using StinkySteak.SimulationTimer;
using UnityEngine;

namespace ProjectTD
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        protected const int HIT_COLLIDER_CACHE_SIZE = 5;

        [Header("General")]
        [SerializeField]
        protected float _damagePoints;
        [SerializeField]
        protected float _speed;
        [SerializeField]
        protected float _lifetime;
        [SerializeField]
        protected LayerMask _targetMask;

        protected Vector3 _direction;
        protected Collider[] _hitColliders;

        private SimulationTimer _projectileLifeTimer;

        private void OnEnable()
        {
            _projectileLifeTimer = SimulationTimer.CreateFromSeconds(_lifetime);
        }

        private void Start()
        {
            _hitColliders = new Collider[HIT_COLLIDER_CACHE_SIZE];
        }

        private void Update()
        {
            Move();
            HitTarget();
            DeactivateProjectile();
        }

        public virtual void SetProjectileDirection(Vector3 direction)
        {
            _direction = direction;
        }

        protected virtual void HitTarget()
        {
            
        }

        protected virtual void ClearHitColliderCache()
        {
            for (int i = 0; i < _hitColliders.Length; i++)
            {
                _hitColliders[i] = null;
            }
        }

        private void Move()
        {
            transform.position += _speed * Time.deltaTime * _direction;
        }

        private void DeactivateProjectile()
        {
            if (_projectileLifeTimer.IsExpired())
            {
                _projectileLifeTimer = SimulationTimer.None;
                gameObject.SetActive(false);
            }
        }
    }
}
