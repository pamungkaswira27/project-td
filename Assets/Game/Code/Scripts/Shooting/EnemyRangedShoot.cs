using JSAM;
using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyRangedShoot : CharacterShoot
    {
        public override IEnumerator FireCoroutine()
        {
            AudioManager.PlaySound(MainSounds.enemy_ranged_attack_effect);
            GameObject projectile = _objectPooler.GetPooledObject("EnemyProjectile", _firingPoint.position, _firingPoint.rotation);

            if (projectile.TryGetComponent(out EnemyProjectile enemyProjectile))
            {
                enemyProjectile.SetProjectileDirection(_firingPoint.forward);
            }

            yield return null;
        }
    }
}
