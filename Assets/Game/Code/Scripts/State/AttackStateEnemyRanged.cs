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

        [Header("Animation Attack")]
        [SerializeField]
        private Animator _animationAttack;

        private SimulationTimer _timerAttack;
        private WaitForSeconds _attackDelay;
        private WaitForSeconds _attackDamage;
        private AIChase _chase;
        private AIPatrol _patrol;
        private float _damageAfterAnimation = 0.5f;

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
            _patrol = GetComponent<AIPatrol>();
            _attackDamage = new WaitForSeconds(_damageAfterAnimation);
        }

        private void FixedUpdate()
        {
            if (GetTarget() != null)
            {
                LookAtPlayer().LookAt(GetTarget().position);

                float distanceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
                bool isRangedAttack = IsRangedAttack(distanceToPlayer);
                bool isMeleeAttack = IsMeleeAttack(distanceToPlayer);

                _animationAttack.SetBool("IsMoving", false);
                _animationAttack.SetBool("IsChasingPlayer", false);

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

            }

            StopAllCoroutines();
            _patrol.IsPatroling = true;
            _chase.enabled = true;
        }

        public override IEnumerator IntervalAttack()
        {
            yield return _attackDelay;

            if (GetTarget() == null) yield return null;


            if (_timerAttack.IsExpired())
            {
                float distance = Vector3.Distance(transform.position, GetTarget().position);
                _timerAttack = SimulationTimer.None;

                if (IsRangedAttack(distance))
                {
                    _animationAttack.SetTrigger("IsAttackingRanged");
                    yield return _attackDamage;
                    Debug.Log("RANGED");
                    _enemyRangedAttack.RangedAttack(_rangedDamage);
                    _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                }

                if (IsMeleeAttack(distance))
                {
                    _animationAttack.SetTrigger("IsAttackingMelee");
                    yield return _attackDamage;
                    Debug.Log("MELEE");
                    _enemyMeleeAttack.MeleeAttack(_meleeDamage);
                    _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                }

                yield return null;
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
