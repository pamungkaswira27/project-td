using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent (typeof (Damageable))]
    public class Props : MonoBehaviour
    {
        [Header("Props Type")]
        [SerializeField]
        private bool _destructible;

        [ShowIf("_destructible")]
        [Header("Number of Hit to Destroy")]
        [SerializeField]
        private int _maxHitCount;

        private int _hitCount;

        private ItemDrop _itemDrop;
        private IDamageable _damageable;

        private void Awake()
        {
            _itemDrop = GetComponent<ItemDrop>();
            _damageable = GetComponent<Damageable>();
        }

        private void Start()
        {
            _damageable.OnDamaged += OnPropsDamaged;
            _hitCount = 0;
        }

        private void OnPropsDamaged(float damageAmount)
        {
            if (!_destructible)
            {
                return;
            }

            _hitCount++;

            if (_hitCount >= _maxHitCount)
            {
                _itemDrop.SpawnRandomItem();
                _hitCount = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
