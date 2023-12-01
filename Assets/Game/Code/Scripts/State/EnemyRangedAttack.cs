using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyRangedAttack : BaseEnemyAttack
    {
        private EnemyRangedShoot _enemyRangedShoot;

        public override void Initialization()
        {
            _enemyRangedShoot = GetComponent<EnemyRangedShoot>();
        }

        public override void RangedAttack(float damaged)
        {
            if (aiFieldOfView.Target == null)
            {
                return;
            }

            if (aiFieldOfView.Target.TryGetComponent(out CharacterHealth characterHealth))
            {
                StartCoroutine(_enemyRangedShoot.FireCoroutine());
            }
        }
    }
}
