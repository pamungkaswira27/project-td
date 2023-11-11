using ProjectTD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class AttackStateEnemySelfExploding : BaseEnemyAttack
    {
        [SerializeField]
        private EnemySelfExplodingAttack _slefExplodingEnemy;
        [Header("Damage")]
        [SerializeField]
        private int _damageSelfExploding;
        [Header("Self Exploding Enemy Distance")]
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _minDistance;

        private void Update()
        {
            if(GetTarget() != null)
            {
                LookAtPlayer().LookAt(GetTarget().position);

                float distanceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
                bool isSelfExplodingAttack = IsSelfExplodingAttack(distanceToPlayer);

                if(isSelfExplodingAttack)
                {
                    _slefExplodingEnemy.SelfExplodingAttack(_damageSelfExploding);
                    return;
                }

                return;
            }
        }

        private bool IsSelfExplodingAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= _maxDistance && distanceToPlayer >= _minDistance && distanceToPlayer < ViewRadius;
        }
    }

}
