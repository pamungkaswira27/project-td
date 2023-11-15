using System;
using UnityEngine;

namespace ProjectTD
{
    public class CharacterHealth : MonoBehaviour, ICharacterHealth
    {
        [Header("Character Health")]
        [SerializeField]
        protected float _healthPoints;
        [SerializeField]
        protected float _maxHealthPoints;

        private IDamageable _damageable;

        public float HealthPoints => _healthPoints;
        public float MaxHealthPoints => _maxHealthPoints;

        public event Action OnCharacterDead;

        private void Start()
        {
            _damageable = GetComponent<IDamageable>();
            _damageable.OnDamaged += OnCharacterDamaged;
        }

        public virtual void DecreaseHealth(float amount)
        {
            _healthPoints -= amount;

            if (_healthPoints <= 0f)
            {
                OnCharacterDead?.Invoke();
            }
        }

        public void SetHealthPoints(float amount)
        {
            _healthPoints = amount;
        }

        public void IncreaseHealthPoints(float amount)
        {
            _healthPoints += amount;

            if (_healthPoints >= _maxHealthPoints)
            {
                _healthPoints = _maxHealthPoints;
            }
        }

        private void OnCharacterDamaged(float damageAmount)
        {
            DecreaseHealth(damageAmount);
        }
    }
}
