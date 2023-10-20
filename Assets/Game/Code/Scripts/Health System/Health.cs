using UnityEngine;

namespace ProjectTD
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private float _healthPoints;

        public void TakeDamage(float damagePoints)
        {
            _healthPoints -= damagePoints;

            if (_healthPoints <= 0f)
            {
                _healthPoints = 0f;
                gameObject.SetActive(false);

                if (!TryGetComponent<BaseCharacter>(out _)) return;

                PlayerManager.Instance.DecreaseLife();
            }
        }
    }
}
