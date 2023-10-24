using UnityEngine;

namespace ProjectTD
{
    [RequireComponent (typeof (Damageable))]
    public class DestructibleProps : MonoBehaviour
    {
        [Header("Number of Hit to Destroy")]
        [SerializeField]
        private int _maxHitCount;

        private int _hitCount;

        private IDamageable _damageable;

        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
        }

        private void Start()
        {
            _damageable.OnDamaged += OnPropsDamaged;
            _hitCount = 0;
        }

        private void OnPropsDamaged(float damageAmount)
        {
            _hitCount++;

            if (_hitCount >= _maxHitCount)
            {
                _hitCount = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
