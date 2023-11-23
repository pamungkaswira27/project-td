using StinkySteak.SimulationTimer;
using System.Collections;
using UnityEngine;

namespace ProjectTD
{
    public class AttackStateEnemySelfExploding : BaseEnemyAttack
    {
        [SerializeField]
        private EnemySelfExplodingAttack _slefExplodingEnemy;
        [SerializeField]
        private LayerMask _target;
        [Header("Damage")]
        [SerializeField]
        private float _minDamage;
        [SerializeField]
        private float _maxDamage;
        [Header("AoE (Area of Effect")]
        [SerializeField]
        private float _radiusExplode;
        [Header("Self Exploding Enemy Distance")]
        [SerializeField]
        private float _maxDistance;
        [SerializeField]
        private float _minDistance;
        [SerializeField]
        private float _timer;

        private Collider[] _inRadiusExplode;
        private AIChase _chase;
        private SimulationTimer _timToExplode;
        private int _insideRadius;
        private bool _isInRadiusExplode;

        public float GetRadiusExplode
        {
            get
            {
                return _radiusExplode;
            }
        }

        private void Awake()
        {
            _chase = GetComponent<AIChase>();
        }

        private void Update()
        {
            if (GetTarget() != null)
            {
                LookAtPlayer().LookAt(GetTarget().position);
                float distanceToPlayer = Vector3.Distance(transform.position, GetTarget().position);
                bool isSelfExplodingAttack = IsSelfExplodingAttack(distanceToPlayer);


                if (isSelfExplodingAttack)
                {
                    _chase.enabled = false;
                    _isInRadiusExplode = true;
                }

                if (_isInRadiusExplode)
                {
                    _chase.enabled = false;
                    OnRadiusExploding();
                    if (_timToExplode.IsRunning) return;
                    _timToExplode = SimulationTimer.CreateFromSeconds(_timer);
                }

                if (!isSelfExplodingAttack && !_isInRadiusExplode)
                {
                    _timToExplode = SimulationTimer.None;
                    _chase.enabled = true;
                }
                return;
            }
        }

        private void OnRadiusExploding()
        {
            _inRadiusExplode = new Collider[COLLIDER_SIZE];
            _insideRadius = Physics.OverlapSphereNonAlloc(transform.position, _radiusExplode, _inRadiusExplode, _target);

            for (int i = 0; i < _insideRadius; i++)
            {
                Collider player = _inRadiusExplode[i];

                if (player == null) return;

                if (player.gameObject.layer == 7)
                {
                    _isInRadiusExplode = true;
                }

                if (_timToExplode.IsExpired()) // maybe at this line is used to Animations. . . . :)
                {
                    DamageToObject(player.gameObject);
                    gameObject.SetActive(false);
                    _timToExplode = SimulationTimer.None;
                }
            }
            _isInRadiusExplode = false;
        }

        private bool IsSelfExplodingAttack(float distanceToPlayer)
        {
            return distanceToPlayer <= _maxDistance && distanceToPlayer > _minDistance && distanceToPlayer < ViewRadius;
        }

        private float CalculateDamage(float distance)
        {
            float normalizedDistance = Mathf.Clamp01((_maxDistance - _minDistance) / (distance - _minDistance));
            float damageArea = Mathf.Lerp(_minDamage, _maxDamage, normalizedDistance);

            return damageArea;
        }

        private void DamageToObject(GameObject obj)
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            float damageExplosion = CalculateDamage(distance);

            _slefExplodingEnemy.SelfExplodingAttack(Mathf.CeilToInt(damageExplosion));
        }

        public override IEnumerator IntervalAttack()
        {
            yield return new WaitForSeconds(_timer);


        }
    }

}
