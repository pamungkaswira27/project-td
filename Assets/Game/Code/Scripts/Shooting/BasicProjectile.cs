using UnityEngine;

namespace ProjectTD
{
    public class BasicProjectile : BaseProjectile
    {
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
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
