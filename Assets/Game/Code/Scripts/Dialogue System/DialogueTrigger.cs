using UnityEngine;

namespace ProjectTD
{
    public class DialogueTrigger : MonoBehaviour
    {
        private const int OVERLAP_RESULT_COLLIDER_SIZE = 1;

        [SerializeField]
        private DialogueSO _dialogue;
        [SerializeField]
        private LayerMask _playerLayerMask;

        private Collider[] _overlapResultColliders;
        private readonly float _overlapRadius = 15f;
        private bool _hasTriggered;

        private void Start()
        {
            _hasTriggered = false;
            _overlapResultColliders = new Collider[OVERLAP_RESULT_COLLIDER_SIZE];
        }

        private void Update()
        {
            TriggerDialogue();
        }

        private void TriggerDialogue()
        {
            if (_hasTriggered)
            {
                return;
            }

            if (_dialogue == null)
            {
                return;
            }

            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, _overlapRadius, _overlapResultColliders, _playerLayerMask);

            for (int i = 0; i < numberOfCollider; i++)
            {
                if (_overlapResultColliders[i] == null)
                {
                    return;
                }

                DialogueManager.Instance.StartDialogue(_dialogue);
                _hasTriggered = true;
            }
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _overlapRadius);
        }
    }
}
