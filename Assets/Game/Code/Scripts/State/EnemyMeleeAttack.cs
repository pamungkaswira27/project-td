using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class EnemyMeleeAttack : BaseEnemyAttack
    {
        public override void MeleeAttack(int damaged)
        {
            //if (aiFieldOfView.Target.TryGetComponent<Health>(out Health playerHealth))
            //{
            //    playerHealth.TakeDamage(damaged);
            //}
            Debug.Log($"Damaged with {damaged}");
            aiFieldOfView.transform.LookAt(aiFieldOfView.Target.position);
        }
    }
}
