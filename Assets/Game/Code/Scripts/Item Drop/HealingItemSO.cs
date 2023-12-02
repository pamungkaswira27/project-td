using UnityEngine;

namespace ProjectTD
{
    [CreateAssetMenu(fileName = "Healing Item", menuName = "ProjectTD/Create Item/Healing Item")]
    public class HealingItemSO : ItemSO
    {
        private const string HEAL_VFX_POOL_TAG = "HealVFX";

        [SerializeField]
        private float _healAmount;

        public override void Use()
        {
            ObjectPooler.Instance.GetPooledObject(HEAL_VFX_POOL_TAG, PlayerManager.Instance.Player.transform.position, Quaternion.Euler(-90f, 0f, 0f));
            PlayerManager.Instance.CharacterHealth.IncreaseHealthPoints(_healAmount);
        }
    }
}
