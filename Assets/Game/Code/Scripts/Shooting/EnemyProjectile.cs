using JSAM;
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

                Vector3 impactPos = _hitColliders[i].ClosestPoint(transform.position);

                if (_hitColliders[i].TryGetComponent(out IDamageable target))
                {
                    AudioManager.PlaySound(MainSounds.enemy_hit_effect_01, impactPos);
                    ObjectPooler.Instance.GetPooledObject("PlayerBloodVFX", _hitColliders[i].ClosestPoint(transform.position), Quaternion.LookRotation(-_direction));
                    target.TryTakeDamage(_damagePoints);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
