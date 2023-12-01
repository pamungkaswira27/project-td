using UnityEngine;

namespace ProjectTD
{
    public class BasicProjectile : BaseProjectile
    {
        private const string BASIC_HIT_IMPACT_POOL_TAG = "BasicHitImpact";

        protected override void HitTarget()
        {
            ClearHitColliderCache();

            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, _hitColliders, _targetMask);

            for (int i = 0; i < numberOfCollider; i++)
            {
                if (_hitColliders[i] == null) continue;

                if (_hitColliders[i].TryGetComponent<Props>(out _))
                {
                    Vector3 impactPos = _hitColliders[i].ClosestPoint(transform.position);
                    ObjectPooler.Instance.GetPooledObject(BASIC_HIT_IMPACT_POOL_TAG, impactPos, Quaternion.identity);
                }

                if (_hitColliders[i].TryGetComponent(out IDamageable target))
                {
                    base.HitTarget();
                    target.TryTakeDamage(_damagePoints);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
