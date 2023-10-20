using UnityEngine;

namespace ProjectTD
{
    public class CheckpointManager : MonoBehaviour
    {
        public static CheckpointManager Instance;

        private Checkpoint _currentCheckpoint;

        public Checkpoint CurrentCheckpoint
        {
            get => _currentCheckpoint;
            set => _currentCheckpoint = value;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
