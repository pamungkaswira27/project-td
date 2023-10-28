using ProjectTD;
using System;
using UnityEngine;

namespace ProjectTD
{
    public class AttackStateEnemyMelee : BaseEnemyAttack
    {
        [SerializeField]
        private EnemyMeleeAttack _attackMelee;
        [Header("Damage")]
        [SerializeField]
        private int _meleeDamage;
        [Header("Attack Melee Distance")]
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _minDistance;

        private void Update()
        {
            if (GetTarget() != null)
            {
                Vector3 inChasing = (transform.position - GetTarget().position);
                float distanceToEnemy = Vector3.Distance(GetTarget().position, transform.position);
                bool isAttacking = IsMeleeDistance(distanceToEnemy);
                bool isEnemyChasing = IsEnemyChasing(inChasing);

                LookAtPlayer().transform.LookAt(GetTarget().transform.position);

                if (isAttacking)
                {
                    //_attackMelee.MeleeAttack(_meleeDamage);
                    Debug.Log("Enemy Melee Attacking");
                    return;
                }

                if (isEnemyChasing)
                {
                    Debug.Log("Enemy Melee Chasing Player");
                    return;
                }

                return;
            }
            Debug.Log("Enemy Melee Patrolling");
        }

        private bool IsMeleeDistance(float distance)
        {
            return distance <= _maxDistance && distance >= _minDistance && distance <= ViewRadius;
        }

        private bool IsEnemyChasing(Vector3 fov)
        {
            bool chasing = Vector3.Angle(transform.position, fov) < ViewAngle / 2;
            return chasing;
        }
    }
}
