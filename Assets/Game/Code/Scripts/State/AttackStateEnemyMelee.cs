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
        [Header("Attack Animation")]
        [SerializeField]
        private Animator _animationAttack;
        [Header("Attack Melee Distance")]
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _minDistance;

        private SimulationTimer _timerAttack;
        private WaitForSeconds _attackDelay;
        private WaitForSeconds _attackDamage;
        private AIChase _chase;
        private AIPatrol _patrol;
        private float _damageAfterAnimation = 0.25f;

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

                float distanceToEnemy = Vector3.Distance(transform.position, GetTarget().position);
                bool isAttacking = IsMeleeDistance(distanceToEnemy);

                _animationAttack.SetBool("IsMoving", false);
                _animationAttack.SetBool("IsChasingPlayer", false);

                if (isAttacking)
                {
                    StartCoroutine(IntervalAttack());
                    _chase.enabled = false;
                    return;
                }

                StopAllCoroutines();
                _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                _animationAttack.SetBool("IsAttackingMelee", false);
                _chase.enabled = true;
            }

            StopAllCoroutines();
            _patrol.IsPatroling = true;
        }

        public override IEnumerator IntervalAttack()
        {
            // yield return _attackDelay;

            if (GetTarget() == null) yield return null;

            if (_timerAttack.IsExpired())
            {
                float distance = Vector3.Distance(transform.position, GetTarget().position);
                _timerAttack = SimulationTimer.None;

                if (IsMeleeDistance(distance))
                {
                    _animationAttack.SetBool("IsAttackingMelee", true);
                    yield return _attackDamage;
                    _attackMelee.MeleeAttack(_meleeDamage);
                    _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                    _animationAttack.SetBool("IsAttackingMelee", false);
                }
            }
        }

        private bool IsMeleeDistance(float distance)
        {
            return distance <= _maxDistance && distance > _minDistance && distance <= ViewRadius;
        }

    }
}
