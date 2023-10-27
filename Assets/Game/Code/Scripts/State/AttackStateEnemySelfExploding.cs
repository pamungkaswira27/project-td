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
                Vector3 inChasingDistance = (transform.position - GetTarget().position);
                float distanceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
                bool isSelfExplodingAttack = IsSelfExplodingAttack(distanceToPlayer);
                bool isEnemyChasing = IsEnemyChasing(inChasingDistance);

                if(isSelfExplodingAttack)
                {
                    Debug.Log("Enemy Self Exploding Attack");
                    _slefExplodingEnemy.SelfExplodingAttack(_damageSelfExploding);
                    return;
                }

                if (isEnemyChasing)
                {
                    Debug.Log("Enemy Self Exploding Chasing Player");
                    return;
                }
                return;
            }
            Debug.Log("Enemy Self Exploding Patrolling");
        }

        private bool IsEnemyChasing(Vector3 inChasingDistance)
        {
            return Vector3.Angle(transform.position, inChasingDistance) < ViewAngle / 2;
        }

        private bool IsSelfExplodingAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= _maxDistance && distanceToPlayer >= _minDistance && distanceToPlayer < ViewRadius;
        }
    }

}
