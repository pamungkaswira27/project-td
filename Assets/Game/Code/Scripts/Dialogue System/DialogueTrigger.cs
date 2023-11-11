using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField]
        private Dialogue _dialogue;
        [SerializeField]
        private bool _useColliderTrigger;
        [SerializeField, ReadOnly]
        private bool _hasTriggered;
        public bool HasTriggered => _hasTriggered;

        private void Start()
        {
            RespawnerManager.Instance.AddDialogueToReset(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_useColliderTrigger || _hasTriggered)
            {
                return;
            }

            if (other.TryGetComponent<BaseCharacter>(out _))
            {
                TriggerDialogue();
            }
        }

        public void TriggerDialogue()
        {
            if (_dialogue.canTriggerObjective)
            {
                InputManager.Instance.DisablePlayerInput();
            }

            DialogueManager.Instance.StartDialogue(_dialogue);
            _hasTriggered = true;
        }

        public void ResetDialogueTrigger()
        {
            _hasTriggered = false;
        }
    }
}
