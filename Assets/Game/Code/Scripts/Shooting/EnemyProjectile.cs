using UnityEngine;

namespace ProjectTD
{
    public class EnemyProjectile : BaseProjectile
    {
        protected override void HitTarget()
        {
            ClearHitColliderCache();

            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, _hitColliders, _targetMask);

            for (int i = 0; i < numberOfCollider; i++)
            {
                if (_hitColliders[i] == null) continue;

                if (_hitColliders[i].TryGetComponent(out IDamageable target))
                {
                    ObjectPooler.Instance.GetPooledObject("PlayerBloodVFX", _hitColliders[i].ClosestPoint(transform.position), Quaternion.LookRotation(-_direction));
                    target.TryTakeDamage(_damagePoints);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
