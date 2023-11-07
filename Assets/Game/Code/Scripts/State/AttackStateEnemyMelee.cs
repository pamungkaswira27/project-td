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

        private Collider[] _otherEnemies;

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
                    Debug.Log("Enemy Melee Attacking");
                    _attackMelee.MeleeAttack(_meleeDamage);
                    return;
                }

                if (isEnemyChasing)
                {
                    GetComponent<AIChase>().enabled = true;
                    return;
                }
                return;
            }
            GetComponent<AIChase>().enabled = false;
            Debug.Log("Enemy Melee Patrolling");
        }

        private bool IsMeleeDistance(float distance)
        {
            return distance <= _maxDistance && distance >= _minDistance && distance <= ViewRadius;
        }

        private bool IsEnemyChasing(Vector3 fov)
        {
            return Vector3.Angle(transform.position, fov) < ViewAngle;
        }

        //private void EnemyMeleeAlert(Vector3 playerPost, string enemyType)
        //{
        //    float rad = ViewRadius - 15;
        //    Collider[] enemies = new Collider[5];
        //    Physics.OverlapSphereNonAlloc(playerPost, rad, enemies, ObstructionMask);

        //    foreach (Collider meleeEnemy in enemies)
        //    {
        //        if (meleeEnemy != null)
        //        {
        //            GameObject enemy = meleeEnemy.gameObject;
        //            if (enemy.GetComponent<AttackStateEnemyMelee>().EnemyType == enemyType)
        //            {
        //                transform.LookAt(GetTarget().position);
        //            }
        //        }
        //    }
        //}
    }
}
