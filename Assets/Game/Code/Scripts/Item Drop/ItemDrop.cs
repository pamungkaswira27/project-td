using UnityEngine;

namespace ProjectTD
{
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField]
        private ItemSO[] _itemsToDrop;
        [SerializeField, Range(0.1f, 1f)]
        private float _dropChance;

        public void SpawnRandomItem()
        {
            if (Random.Range(0f, 1f) <= _dropChance)
            {
                int randomIndex = Random.Range(0, _itemsToDrop.Length);
                ObjectPooler.Instance.GetPooledObject(_itemsToDrop[randomIndex].name, transform.position, Quaternion.identity);
            }
        }
    }
}
