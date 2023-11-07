using ProjectTD;
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
        public Transform _player;
        public bool _isAttacked;

        private void Start()
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }

        private void LateUpdate()
        {
            if (_isAttacked)
            {
                _playerManager = FindObjectOfType<PlayerManager>();

                if (_playerManager != null)
                {
                    Transform post = _playerManager.GetTransformPlayer();

                    if (post != null)
                    {
                        _player = post;
                        Vector3 playerPost = _player.position;

                        CheckOtherEnemies();

                        if (IsSelfExplode())
                        {
                            EnemiesAlert(playerPost);
                            return;
                        }

                        if (IsMelee())
                        {
                            EnemiesAlert(playerPost);
                            return;
                        }

                        if (IsRanged())
                        {
                            EnemiesAlert(playerPost);
                            return;
                        }

                    }
                }
            }
        }

        public void OnAttacked()
        {
            _isAttacked = true;
        }

        private void EnemiesAlert(Vector3 player)
        {
            transform.LookAt(player);
        }

        private void CheckOtherEnemies()
        {
            otherEnemies = new Collider[10];
            int a = Physics.OverlapSphereNonAlloc(transform.position, _radiusForTrigger, otherEnemies, _enemies);
            Debug.Log(a);

            for (int i = 0; i < a; i++)
            {
                Collider enemy = otherEnemies[i];

                if (enemy != null)
                {
                    Debug.Log(enemyType);
                    Debug.Log(enemy);
                    AIAlertSystem otherEnemy = enemy.gameObject.GetComponent<AIAlertSystem>();

                    Debug.Log(otherEnemy.enemyType);
                    if (otherEnemy != null && otherEnemy.enemyType == enemyType)
                    {
                        Debug.Log("Attackkk");
                        otherEnemy.OnAttacked();
                        transform.LookAt(_player.position);
                        return;
                    }
                    OnAttacked();
                    transform.LookAt(_player.position);
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
