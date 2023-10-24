using UnityEngine;

namespace ProjectTD
{
    public class UltimateProjectile : BaseProjectile
    {
        [Header("Ultimate")]
        [SerializeField]
        private int _maxHitCount;

        private int _hitCount;

        protected override void HitTarget()
        {
            int hitColliderSize = 10;
            Collider[] hitColliders = new Collider[hitColliderSize];
            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, hitColliders, _targetMask);

            for (int i = 0; i < numberOfCollider; i++)
            {
                if (hitColliders[i].TryGetComponent(out IDamageable target))
                {
                    target.TryTakeDamage(_damagePoints);
                }

                _hitCount++;

                if (_hitCount > _maxHitCount)
                {
                    _hitCount = 0;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
