using ProjectTD;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{

    public class AIAlertSystem : BaseEnemyAttack
    {
        [SerializeField]
        private PlayerManager _playerManager;
        [SerializeField]
        private LayerMask _enemies;
        [SerializeField]
        private float _radiusForTrigger;
        [SerializeField]
        private string enemyType;

        private Collider[] otherEnemies;
        private Vector3 _playerPosition;
        private Transform _player;
        private Transform _post;
        private bool _isAttacked;
        public bool _deadRangedEnemy;

        public float AlertRadius
        {
            get
            {
                return _radiusForTrigger;
            }
        }

        public string EnemyType
        {
            get
            {
                return enemyType;
            }
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

            _post = _playerManager.GetTransformPlayer();

            if (_post == null) return;

            _player = _post;
            _playerPosition = _player.position;

            Debug.Log(enemyType);

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

        public void OnAttacked()
        {
            _isAttacked = true;
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
            otherEnemies = new Collider[10];
            int enemyInRadius = Physics.OverlapSphereNonAlloc(transform.position, _radiusForTrigger, otherEnemies, _enemies);
            Debug.Log(enemyInRadius);

            for (int i = 0; i < enemyInRadius; i++)
            {
                Collider enemy = otherEnemies[i];
                if (enemy != null)
                {
                    if (!enemy.gameObject.TryGetComponent<AIAlertSystem>(out var otherEnemy)) return;

                    if (otherEnemy.enemyType == enemyType)
                    {
                        otherEnemy._deadRangedEnemy = true;
                        otherEnemy.OnAttacked();
                        transform.LookAt(_player.position);
                    }
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
