using UnityEngine;

namespace ProjectTD
{
    public class Checkpoint : MonoBehaviour
    {
        private bool _isReached;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BaseCharacter>(out _))
            {
                if (_isReached) return;

                CheckpointManager.Instance.CurrentCheckpoint = this;
                PlayerManager.Instance.PlayerSpawnPoint = CheckpointManager.Instance.CurrentCheckpoint.transform;
                _isReached = true;
            }
        }
    }
}
