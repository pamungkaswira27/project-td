using UnityEngine;

namespace ProjectTD
{
    public class UltimateProjectile : BaseProjectile
    {
        [Header("Ultimate")]
        [SerializeField]
        private int _maxHitCount;

        private int _hitCount;

        private void OnTriggerEnter(Collider other)
        {
            bool isTriggerWithCharacter = other.TryGetComponent<CharacterAim>(out _);
            bool isTriggerWithProjectile = other.TryGetComponent<BaseProjectile>(out _);

            if (!isTriggerWithCharacter && !isTriggerWithProjectile)
            {
                if (other.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    target.TakeDamage(_damagePoints);
                }

                _hitCount++;

                if (_hitCount > _maxHitCount)
                {
                    _hitCount = 0;
                    DeactivateProjectile();
                }
            }
        }
    }
}
