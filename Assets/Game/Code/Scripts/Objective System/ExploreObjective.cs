using UnityEngine;

namespace ProjectTD
{
    public class ExploreObjective : BaseObjective
    {
        [SerializeField]
        private Transform _destination;

        private readonly float _minimumDistanceToReached = 5.5f;

        public override void OnStart()
        {
            base.OnStart();
            _hasStarted = true;
        }

        public override bool IsObjectiveCompleted()
        {
            Vector3 playerPosition = PlayerManager.Instance.Player.transform.position;

            return (Vector3.Distance(_destination.position, playerPosition) <= _minimumDistanceToReached);
        }
    }
}
