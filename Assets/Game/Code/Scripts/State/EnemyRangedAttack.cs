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
            Debug.Log($"Attacked by Ranged Type, with {damaged} damage's");
        }
    }
}
