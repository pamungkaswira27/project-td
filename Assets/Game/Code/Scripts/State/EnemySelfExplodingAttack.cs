using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public class EnemySelfExplodingAttack : BaseEnemyAttack
    {
        private const string ENEMY_SUICIDE_VFX_POOL_TAG = "EnemySuicideVFX";

        public override void SelfExplodingAttack(float damage)
        {
            if (aiFieldOfView.Target == null)
            {
                return;
            }

            if (aiFieldOfView.Target.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                AudioManager.PlaySound(MainSounds.enemy_suicide_attack_effect);
                ObjectPooler.Instance.GetPooledObject(ENEMY_SUICIDE_VFX_POOL_TAG, transform.position, Quaternion.identity);
                playerHealth.DecreaseHealth(damage);
            }
        }
    }
}
