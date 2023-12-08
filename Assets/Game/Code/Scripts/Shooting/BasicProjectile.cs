using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public class BasicProjectile : BaseProjectile
    {
        private const string BASIC_HIT_IMPACT_POOL_TAG = "BasicHitImpact";
        private const string ENEMY_HIT_IMPACT_POOL_TAG = "EnemyHitImpact";

        protected override void HitTarget()
        {
            ClearHitColliderCache();

            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, 0.5f, _hitColliders, _targetMask);

            for (int i = 0; i < numberOfCollider; i++)
            {
                if (_hitColliders[i] == null) continue;

                Vector3 impactPos = _hitColliders[i].ClosestPoint(transform.position);

                if (_hitColliders[i].TryGetComponent<Props>(out _))
                {
                    ObjectPooler.Instance.GetPooledObject(BASIC_HIT_IMPACT_POOL_TAG, impactPos, Quaternion.identity);
                }

                if (_hitColliders[i].TryGetComponent<EnemyHealth>(out _))
                {
                    ObjectPooler.Instance.GetPooledObject(ENEMY_HIT_IMPACT_POOL_TAG, impactPos, Quaternion.identity);
                }

                if (_hitColliders[i].TryGetComponent(out IDamageable target))
                {
                    AudioManager.PlaySound(MainSounds.enemy_hit_effect_01, impactPos);
                    target.TryTakeDamage(_damagePoints);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
