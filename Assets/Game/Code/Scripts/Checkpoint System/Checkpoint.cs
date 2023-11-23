using UnityEngine;

namespace ProjectTD
{
    public class Checkpoint : MonoBehaviour
    {
        private const int COLLIDER_CACHE_SIZE = 5;

        [SerializeField, Range(5f, 10f)]
        private float _checkpointRadius;
        [SerializeField]
        private LayerMask _playerLayerMask;
        private Collider[] _overlapResultColliders;
        private bool _isReached;

        private void Start()
        {
            _overlapResultColliders = new Collider[COLLIDER_CACHE_SIZE];
        }

        private void Update()
        {
            ClearColliderCache();
            SetCheckpoint();
        }

        private void ClearColliderCache()
        {
            for (int i = 0; i <  _overlapResultColliders.Length; i++)
            {
                _overlapResultColliders[i] = null;
            }
        }

        private void SetCheckpoint()
        {
            if (_isReached) return;

            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, _checkpointRadius, _overlapResultColliders, _playerLayerMask);

            for (int i = 0; i <= numberOfCollider; i++)
            {
                if (_overlapResultColliders[i] == null) break;

                if (_overlapResultColliders[i].TryGetComponent<BaseCharacter>(out _))
                {
                    CheckpointManager.Instance.CurrentCheckpoint = this;
                    PlayerManager.Instance.PlayerSpawnPoint = CheckpointManager.Instance.CurrentCheckpoint.transform;
                    RespawnerManager.Instance.ClearList();
                    _isReached = true;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _checkpointRadius);
        }
    }
}
