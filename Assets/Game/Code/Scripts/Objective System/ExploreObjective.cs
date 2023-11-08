using UnityEngine;

namespace ProjectTD
{
    public class ExploreObjective : Objective
    {
        [Header("Explore Objective")]
        [SerializeField]
        private Transform _destination;
        public Transform Destination => _destination;

        public override bool IsObjectiveCompleted()
        {
            Vector3 playerPosition = PlayerManager.Instance.Player.transform.position;
            Vector3 destinationPosition = Destination.position;

            return (Vector3.Distance(playerPosition, destinationPosition) <= 5f);
        }
    }
}
