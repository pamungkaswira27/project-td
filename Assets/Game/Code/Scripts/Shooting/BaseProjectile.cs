using StinkySteak.SimulationTimer;
using UnityEngine;

namespace ProjectTD
{
    public abstract class BaseProjectile : MonoBehaviour
    {
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

        private SimulationTimer _projectileLifeTimer;

        private void OnEnable()
        {
            _projectileLifeTimer = SimulationTimer.CreateFromSeconds(_lifetime);
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
            // Implementation in child class
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
