using ProjectTD;
using StinkySteak.SimulationTimer;
using System;
using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class AttackStateEnemyMelee : BaseEnemyAttack
    {
        [SerializeField]
        private EnemyMeleeAttack _attackMelee;
        [Header("Damage")]
        [SerializeField]
        private float _meleeDamage;
        [Header("Attack Speed")]
        [SerializeField]
        private float _attackSpeed;
        [Header("Attack Melee Distance")]
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _minDistance;

        private SimulationTimer _timerAttack;
        private WaitForSeconds _attackDelay;
        private AIChase _chase;

        private void OnEnable()
        {
            _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
        }

        public override void Initialization()
        {
            base.Initialization();
            _attackDelay = new WaitForSeconds(_attackSpeed);
        }

        private void Awake()
        {
            _chase = GetComponent<AIChase>();
        }

        private void FixedUpdate()
        {
            if (GetTarget() != null)
            {
                LookAtPlayer().LookAt(GetTarget().position);

                float distanceToEnemy = Vector3.Distance(GetTarget().position, transform.position);
                bool isAttacking = IsMeleeDistance(distanceToEnemy);

                if (isAttacking)
                {
                    StartCoroutine(IntervalAttack());
                    _chase.enabled = false;
                    return;
                }
                _chase.enabled = true;
                StopAllCoroutines();
                return;
            }
        }

        public override IEnumerator IntervalAttack()
        {
            yield return _attackDelay;

            if (_timerAttack.IsExpired())
            {
                _attackMelee.MeleeAttack(_meleeDamage);
                _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
            }
        }

        private bool IsMeleeDistance(float distance)
        {
            return distance <= _maxDistance && distance >= _minDistance && distance <= ViewRadius;
        }

    }
}
