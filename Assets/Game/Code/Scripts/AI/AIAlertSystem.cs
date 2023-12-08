using UnityEngine;

namespace ProjectTD
{

    public class AIAlertSystem : BaseEnemyAttack
    {
        [SerializeField]
        private LayerMask _enemies;
        [SerializeField]
        private float _radiusForTrigger;
        [SerializeField]
        private string enemyType;

        private PlayerManager _playerManager;
        private Collider[] otherEnemies;
        private Vector3 _playerPosition;
        private Transform _player;
        private Transform _post;
        private bool _isAttacked;
        private bool _deadRangedEnemy;

        public float AlertRadius
        {
            get { return _radiusForTrigger; }
        }

        public string EnemyType
        {
            get { return enemyType; }
        }

        private void Start()
        {
            _playerManager = PlayerManager.Instance;
            EnemyHealth.RangedEnemyDead += OnDeadEnemyRanged;
        }

        private void Update()
        {
            if (!_isAttacked) return;

            if (_playerManager == null) return;

            _post = _playerManager.Player.transform;

            if (_post == null) return;

            _player = _post;
            _playerPosition = _player.position;

            if (IsSelfExplode())
            {
                CheckOtherEnemies();
                return;
            }

            if (IsMelee())
            {
                CheckOtherEnemies();
                return;
            }

            if (IsRanged())
            {
                transform.LookAt(_playerPosition);
                return;
            }

            if (!_deadRangedEnemy) return;

            OnDeadEnemyRanged();
        }

        public bool OnAttacked()
        {
            return _isAttacked = true;
        }

        public bool NotAttacked()
        {
            return _isAttacked = false;
        }

        public void OnDeadEnemyRanged()
        {
            if (enemyType != "Ranged") return;
            if (_post == null) return;

            _player = _post;
            _playerPosition = _player.position;

            CheckOtherEnemies();
        }

        private void CheckOtherEnemies()
        {
            otherEnemies = new Collider[COLLIDER_SIZE];

            Vector3 playerPost = (_playerPosition - transform.position);
            Quaternion lookPlayer = Quaternion.LookRotation(playerPost);
            int enemyInRadius = Physics.OverlapSphereNonAlloc(transform.position, _radiusForTrigger, otherEnemies, _enemies);

            for (int i = 0; i < enemyInRadius; i++)
            {
                Collider enemy = otherEnemies[i];
                if (enemy == null) return;

                if (!enemy.gameObject.TryGetComponent<AIAlertSystem>(out var otherEnemy)) return;

                if (enemyType == otherEnemy.enemyType)
                {
                    otherEnemy._deadRangedEnemy = true;
                    otherEnemy.OnAttacked();
                }

                if (otherEnemy.OnAttacked())
                {
                    transform.rotation = lookPlayer;
                    return;
                }
            }
        }

        private bool IsMelee()
        {
            return GetEnemyMeleeType() == enemyType;
        }

        private bool IsRanged()
        {
            return GetEnemyRangedType() == enemyType;
        }

        private bool IsSelfExplode()
        {
            return GetEnemySelfExplodingType() == enemyType;
        }
    }
}
