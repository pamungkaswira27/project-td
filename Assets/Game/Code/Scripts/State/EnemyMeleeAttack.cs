using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyMeleeAttack : BaseEnemyAttack
    {
        public override void MeleeAttack(float damaged)
        {
            if (aiFieldOfView.Target.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                playerHealth.DecreaseHealth(damaged);
            }
        }
    }
}
