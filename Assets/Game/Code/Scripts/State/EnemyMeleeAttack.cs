using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyMeleeAttack : BaseEnemyAttack
    {
        public override void MeleeAttack(int damaged)
        {
            Debug.Log($"Attacked by Melee Type, with {damaged} damage's.");
            aiFieldOfView.transform.LookAt(aiFieldOfView.Target.position);
        }
    }
}
