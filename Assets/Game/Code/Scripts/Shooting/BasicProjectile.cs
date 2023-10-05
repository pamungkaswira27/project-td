using UnityEngine;

namespace ProjectTD
{
    public class BasicProjectile : BaseProjectile
    {
        private void OnTriggerEnter(Collider other)
        {
            bool isTriggerWithCharacter = other.TryGetComponent<CharacterAim>(out _);
            bool isTriggerWithProjectile = other.TryGetComponent<BaseProjectile>(out _);

            if (!isTriggerWithCharacter && !isTriggerWithProjectile)
            {
                if (other.TryGetComponent<IDamageable>(out IDamageable target))
                {
                    target.TakeDamage(_damagePoints);
                    DeactivateProjectile();
                }
            }
        }
    }
}
