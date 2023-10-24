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

        public float HealthPoints 
        { 
            get => _healthPoints; 
            set => _healthPoints = value; 
        }

        public float MaxHealthPoints => _maxHealthPoints;

        private void Awake()
        {
            _damageable = GetComponent<IDamageable>();
        }

        private void Start()
        {
            _damageable.OnDamaged += OnCharacterDamaged;
        }

        public virtual void DecreaseHealth(float amount)
        {
            _healthPoints -= amount;
        }

        private void OnCharacterDamaged(float damageAmount)
        {
            DecreaseHealth(damageAmount);
        }
    }
}
