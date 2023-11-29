using ProjectTD;
using StinkySteak.SimulationTimer;
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
        private float _rangedDamage;
        [SerializeField]
        private float _meleeDamage;
        [Header("Attack Speed")]
        [SerializeField]
        private float _attackSpeed;
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

        private SimulationTimer _timerAttack;
        private WaitForSeconds _attackDelay;
        private AIChase _chase;

        public float AttackSpeed => _attackSpeed;

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

                float distanceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
                bool isRangedAttack = IsRangedAttack(distanceToPlayer);
                bool isMeleeAttack = IsMeleeAttack(distanceToPlayer);

                if (isRangedAttack)
                {
                    StartCoroutine(IntervalAttack());
                    _chase.enabled = false;
                    return;
                }

                if (isMeleeAttack)
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
            float distance = Vector3.Distance(transform.position, GetTarget().position);

            if (_timerAttack.IsExpired())
            {
                if (IsRangedAttack(distance))
                {
                    _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                    _enemyRangedAttack.RangedAttack(_rangedDamage);
                    yield return null;
                }

                if (IsMeleeAttack(distance))
                {
                    _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                    _enemyMeleeAttack.MeleeAttack(_meleeDamage);
                    yield return null;
                }
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
