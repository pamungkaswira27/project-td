using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    public class CollectObjective : Objective
    {
        [Header("Collect Objective")]
        [SerializeField]
        private int _requiredAmount;
        public int RequiredAmount => _requiredAmount;
        [SerializeField, ReadOnly]
        private int _currentAmount;
        public int CurrentAmount => _currentAmount;

        public override bool IsObjectiveCompleted()
        {
            return (CurrentAmount >= RequiredAmount);
        }

        public void UpdateCurrentAmount()
        {
            _currentAmount++;
        }
    }
}
