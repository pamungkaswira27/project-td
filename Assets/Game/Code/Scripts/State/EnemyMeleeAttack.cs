using JSAM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyMeleeAttack : BaseEnemyAttack
    {
        public override void MeleeAttack(float damaged)
        {
            if (aiFieldOfView.Target == null)
            {
                return;
            }

            if (aiFieldOfView.Target.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                ObjectPooler.Instance.GetPooledObject("PlayerBloodVFX", playerHealth.transform.position, Quaternion.identity);
                AudioManager.PlaySound(MainSounds.enemy_melee_attack_effect);
                playerHealth.DecreaseHealth(damaged);
            }
        }
    }
}
