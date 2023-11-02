using ProjectTD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyRangedAttack : BaseEnemyAttack
    {
        public override void RangedAttack(int damaged)
        {
            if (aiFieldOfView.Target.TryGetComponent(out CharacterHealth characterHealth))
            {
                characterHealth.DecreaseHealth(damaged);
            }
        }
    }
}
