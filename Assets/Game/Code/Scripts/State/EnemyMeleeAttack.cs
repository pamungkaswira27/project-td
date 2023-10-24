using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyMeleeAttack : BaseEnemyAttack
    {
        public override void MeleeAttack(int damaged)
        {
            if (aiFieldOfView.Target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TryTakeDamage(damaged);
            }
        }
    }
}
