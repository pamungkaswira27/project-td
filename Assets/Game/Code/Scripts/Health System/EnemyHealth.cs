using UnityEngine;

namespace ProjectTD
{
    [RequireComponent(typeof(Damageable))]
    public class EnemyHealth : CharacterHealth
    {
        [SerializeField]
        private AIAlertSystem _aiAlert;

        public delegate void OnDeath();
        public static event OnDeath RangedEnemyDead;

        public override void DecreaseHealth(float amount)
        {
            ObjectPooler.Instance.GetPooledObject("EnemyBloodVFX", transform.position, Quaternion.identity);
            base.DecreaseHealth(amount);

            if (_healthPoints <= 0f)
            {
                if (_aiAlert.EnemyType == TagConst.TAG_ENEMY_RANGED)
                {
                    _healthPoints = 0f;
                    RangedEnemyDead?.Invoke();
                    gameObject.SetActive(false);
                }

                gameObject.SetActive(false);
            }
            else
            {
                _aiAlert.OnAttacked();
            }
        }
    }
}
