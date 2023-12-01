using UnityEngine;

namespace ProjectTD
{
    public class CollectObjective : BaseObjective
    {
        [SerializeField]
        private GameObject[] _itemsToCollect;

        private int _amountToCollect;

        public override void Initialize()
        {
            _amountToCollect = _itemsToCollect.Length;
        }

        public override void OnStart()
        {
            base.OnStart();
            _hasStarted = true;
        }

        public override void OnUpdate()
        {
            for (int i = 0; i < _itemsToCollect.Length; i++)
            {
                if (_itemsToCollect[i] == null)
                {
                    continue;
                }

                if (!_itemsToCollect[i].activeInHierarchy)
                {
                    _amountToCollect--;
                }
            }
        }

        public override bool IsObjectiveCompleted()
        {
            return (_amountToCollect <= 0);
        }
    }
}
