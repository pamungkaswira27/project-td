using UnityEngine;

namespace ProjectTD
{
    public class DialogueTrigger : MonoBehaviour
    {
        private const int OVERLAP_RESULT_COLLIDER_SIZE = 1;

        [SerializeField]
        private DialogueSO _dialogue;

        private Collider[] _overlapResultColliders;
        private readonly float _overlapRadius = 10f;
        private bool _hasTriggered;

        private void Start()
        {
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

            int numberOfCollider = Physics.OverlapSphereNonAlloc(transform.position, _overlapRadius, _overlapResultColliders);

            for (int i = 0; i < numberOfCollider; i++)
            {
                if (_overlapResultColliders[i] == null)
                {
                    return;
                }

                if (_overlapResultColliders[i].TryGetComponent<BaseCharacter>(out _))
                {
                    DialogueManager.Instance.StartDialogue(_dialogue);
                    _hasTriggered = true;
                }
            }
        }
    }
}
