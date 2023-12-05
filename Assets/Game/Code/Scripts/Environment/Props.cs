using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    [RequireComponent (typeof (Damageable))]
    public class Props : MonoBehaviour
    {
        private const string DESTRUCTIBLE_VFX_POOL_TAG = "DestructibleVFX";

        [Header("Props Type")]
        [SerializeField]
        private bool _destructible;

        [ShowIf("_destructible")]
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
            if (!_destructible)
            {
                return;
            }

            _hitCount++;

            if (_hitCount >= _maxHitCount)
            {
                ObjectPooler.Instance.GetPooledObject(DESTRUCTIBLE_VFX_POOL_TAG, transform.position, Quaternion.identity);

                if (TryGetComponent(out Barrel barrel))
                {
                    barrel.Explode();
                }

                if (TryGetComponent(out ItemDrop itemDrop))
                {
                    itemDrop.SpawnRandomItem();
                }
                
                _hitCount = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
