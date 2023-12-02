using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public class AIAlertOnAttack : BaseEnemyAttack
    {   
        [SerializeField]
        private LayerMask _enemies;
        [SerializeField]
        private float _radiusForTrigger;
        [SerializeField]
        private string enemyType;

        private Collider[] otherEnemies;
        private PlayerManager _playerManager;
        public Transform _player;
        public bool _isAttacked;


        private void Start()
        {
            _playerManager = PlayerManager.Instance;
        }

        private void LateUpdate()
        {
            if (_isAttacked)
            {
                if (_playerManager != null)
                {
                    Transform post = _playerManager.Player.transform;

                    if (post != null)
                    {
                        _player = post;
                        Vector3 playerPost = _player.position;
                        CheckOtherEnemies();
                        Debug.DrawRay(transform.position, transform.forward * _radiusForTrigger, Color.red);

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
            //otherEnemies = new Collider[10];
            //int enemies = Physics.OverlapSphereNonAlloc(transform.position, _radiusForTrigger, otherEnemies, _enemies);

            //for (int i = 0; i < enemies; i++)
            //{
            //    Collider enemy = otherEnemies[i];
            //    if (enemy.gameObject != gameObject && enemy.gameObject.layer == _enemies)
            //    {
            //        if (EnemyType == enemiesType)
            //        {
            //            transform.LookAt(_player.position);
            //            Debug.Log("CHASING");
            //        }
            //        return;
            //    }
            //}

            transform.LookAt(player);
        }

        private void CheckOtherEnemies()
        {
            otherEnemies = new Collider[10];
            int a = Physics.OverlapSphereNonAlloc(transform.position, _radiusForTrigger, otherEnemies, _enemies);

            for (int i = 0; i < a; i++)
            {
                Collider enemy = otherEnemies[i];

                if (enemy != null)
                {
                    AIAlertOnAttack otherEnemy = enemy.gameObject.GetComponent<AIAlertOnAttack>();

                    if (otherEnemy != null && otherEnemy.enemyType == enemyType)
                    {
                        otherEnemy.OnAttacked();
                        transform.LookAt(_player.position);
                        return;
                    }
                    OnAttacked();
                    transform.LookAt(_player.position);
                    return;
                }
            }

            //foreach (Collider enemy in otherEnemies)
            //{
            //    if (enemy != null && enemy.gameObject.layer == _enemies)
            //    {
            //        Debug.Log(enemyType);
            //        Debug.Log(enemy);
            //        AIAlertOnAttack otherEnemy = enemy.gameObject.GetComponent<AIAlertOnAttack>();

            //        Debug.Log(otherEnemy.enemyType);
            //        if (otherEnemy != this && otherEnemy.enemyType == enemyType)
            //        {
            //            otherEnemy.OnAttacked();
            //            transform.LookAt(_player.position);
            //            return;
            //        }
            //        return;
            //    }
            //    OnAttacked();
            //    return;
            //}
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
