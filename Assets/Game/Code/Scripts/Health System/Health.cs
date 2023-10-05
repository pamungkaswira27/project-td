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
                Debug.Log($"[{this.GetType().Name}] {gameObject.name} is dead.");
            }
        }
    }
}
