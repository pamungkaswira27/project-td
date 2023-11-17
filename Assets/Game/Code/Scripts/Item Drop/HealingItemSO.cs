using UnityEngine;

namespace ProjectTD
{
    [CreateAssetMenu(fileName = "Healing Item", menuName = "ProjectTD/Create Item/Healing Item")]
    public class HealingItemSO : ItemSO
    {
        [SerializeField]
        private float _healAmount;

        public override void Use()
        {
            PlayerManager.Instance.CharacterHealth.IncreaseHealthPoints(_healAmount);
        }
    }
}
