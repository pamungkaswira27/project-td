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
        [Header("Radius Chasing Melee Distance")]
        [SerializeField]
        private float _divideForRadiusChasing;
        [Header("Attack Melee Distance")]
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _minDistance;

        private void Update()
        {
            if (GetTarget() != null)
            {
                LookAtPlayer().LookAt(GetTarget().position);

                float distanceToEnemy = Vector3.Distance(GetTarget().position, transform.position);
                bool isAttacking = IsMeleeDistance(distanceToEnemy);

                if (isAttacking)
                {
                    _attackMelee.MeleeAttack(_meleeDamage);
                    return;
                }

                return;
            }
        }

        private bool IsMeleeDistance(float distance)
        {
            return distance <= _maxDistance && distance >= _minDistance && distance <= ViewRadius;
        }

    }
}
