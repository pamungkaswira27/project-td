using ProjectTD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class AttackStateEnemyRanged : BaseEnemyAttack
    {
        [SerializeField]
        private EnemyRangedAttack _enemyRangedAttack;
        [SerializeField]
        private EnemyMeleeAttack _enemyMeleeAttack;
        [Header("Damage")]
        [SerializeField]
        private int _rangedDamage;
        private int _meleeDamage;
        [Header("Attack Ranged Distance")]
        [SerializeField]
        private float _maxRangedDistance;
        [SerializeField]
        private float _minRangedDistance;
        [Header("Attack Melee Distance")]
        [SerializeField]
        private float _maxMeleeDistance;
        [SerializeField]
        private float _minMeleeDistance;

        private void Update()
        {
            if (GetTarget() != null)
            {
                LookAtPlayer().LookAt(GetTarget().position);

                float distanceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
                bool isRangedAttack = IsRangedAttack(distanceToPlayer);
                bool isMeleeAttack = IsMeleeAttack(distanceToPlayer);

                if (isRangedAttack)
                {
                    _enemyRangedAttack.RangedAttack(_rangedDamage);
                    return;
                }

                if (isMeleeAttack)
                {
                    _enemyMeleeAttack.MeleeAttack(_meleeDamage);
                    return;
                }
                return;
            }
           

        }

        private bool IsMeleeAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= _maxMeleeDistance && distanceToPlayer > _minMeleeDistance && distanceToPlayer <= ViewRadius;
        }

        private bool IsRangedAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= _maxRangedDistance && distanceToPlayer > _minRangedDistance && distanceToPlayer <= ViewRadius;
        }
    }

}
