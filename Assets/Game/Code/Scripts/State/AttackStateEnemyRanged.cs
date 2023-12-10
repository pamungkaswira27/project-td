using StinkySteak.SimulationTimer;
using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class AttackStateEnemyRanged : BaseEnemyAttack
    {
        [SerializeField]
        private EnemyRangedAttack _enemyRangedAttack;

        [Header("Damage")]
        [SerializeField]
        private float _rangedDamage;

        [Header("Attack Speed")]
        [SerializeField]
        private float _attackSpeed;

        [Header("Attack Ranged Distance")]
        [SerializeField]
        private float _maxRangedDistance;
        [SerializeField]
        private float _minRangedDistance;

        [Header("Animation Attack")]
        [SerializeField]
        private Animator _animationAttack;
        [SerializeField]
        private Animator _bowAnimator;

        private SimulationTimer _timerAttack;
        private WaitForSeconds _attackDelay;
        private WaitForSeconds _attackDamage;
        private AIChase _chase;
        private AIPatrol _patrol;
        private float _damageAfterAnimation = 1f;

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

                _animationAttack.SetBool("IsMoving", false);
                _animationAttack.SetBool("IsChasingPlayer", false);

                if (isRangedAttack)
                {
                    StartCoroutine(IntervalAttack());
                    _chase.enabled = false;
                    _patrol.IsPatroling = false;
                    return;
                }

                StopAllCoroutines();
                _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                _animationAttack.SetBool("IsAttackingRanged", false);
                _chase.enabled = true;
            }

            StopAllCoroutines();
            _patrol.IsPatroling = true;
            _chase.enabled = true;
        }

        public override IEnumerator IntervalAttack()
        {
            // yield return _attackDelay;

            if (GetTarget() == null) yield return null;


            if (_timerAttack.IsExpired())
            {
                float distance = Vector3.Distance(transform.position, GetTarget().position);
                _timerAttack = SimulationTimer.None;

                if (IsRangedAttack(distance))
                {
                    _bowAnimator.SetTrigger("attack");
                    _animationAttack.SetBool("IsAttackingRanged", true);
                    yield return _attackDamage;
                    _enemyRangedAttack.RangedAttack(_rangedDamage);
                    _timerAttack = SimulationTimer.CreateFromSeconds(_attackSpeed);
                    _animationAttack.SetBool("IsAttackingRanged", false);
                    yield return null;
                }

                _animationAttack.SetBool("IsAttackingRanged", false);
                yield return null;
            }
        }

        private bool IsRangedAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= _maxRangedDistance && distanceToPlayer > _minRangedDistance && distanceToPlayer <= ViewRadius;
        }
    }

}
