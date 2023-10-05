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
        protected Rigidbody _rigidbody;

        protected Vector3 _direction;

        private void OnEnable()
        {
            Invoke(nameof(DeactivateProjectile), _lifetime);
        }

        private void Update()
        {
            transform.position += _speed * Time.deltaTime * _direction;
        }

        public virtual void SetProjectileDirection(Vector3 direction)
        {
            _direction = direction;
        }

        protected void DeactivateProjectile()
        {
            gameObject.SetActive(false);
        }
    }
}
