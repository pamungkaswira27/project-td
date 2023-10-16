using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemySelfExplodingAttack : BaseEnemyAttack
    {
        public override void SelfExplodingAttack(int damage)
        {
            Debug.Log($"Attacked by Self Exploding Type, with {damage} damage's.");
            aiFieldOfView.transform.LookAt(aiFieldOfView.Target.position);
        }
    }
}
