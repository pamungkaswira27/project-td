using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemySelfExplodingAttack : BaseEnemyAttack
    {
        public override void SelfExplodingAttack(float damage)
        {
            if(aiFieldOfView.Target.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                playerHealth.DecreaseHealth(damage);
            }
        }
    }
}
