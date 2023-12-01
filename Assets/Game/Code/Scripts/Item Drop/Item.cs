using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public class Item : MonoBehaviour
    {
        private const int OVERLAP_RESULT_COLLIDER_SIZE = 1;

        [SerializeField]
        private ItemSO _item;
        [SerializeField, Range(0.1f, 10f)]
        private float _overlapRadius = 5f;
        [SerializeField]
        private LayerMask _playerLayerMask;

        private Collider[] _overlapResultColliders;

        private void Start()
        {
            _overlapResultColliders = new Collider[OVERLAP_RESULT_COLLIDER_SIZE];
        }

        private void Update()
        {
            TriggerItem();
        }

        private void TriggerItem()
        {
            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, _overlapRadius, _overlapResultColliders, _playerLayerMask);

            if (numberOfCollider == 0)
            {
                return;
            }

            AudioManager.PlaySound(MainSounds.item_pickup);
            _item.Use();
            gameObject.SetActive(false);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _overlapRadius);
        }
    }
}
