using UnityEngine;

namespace ProjectTD
{
    public class DialogueOnlyObjective : BaseObjective
    {
        [SerializeField]
        private DialogueSO _dialogue;

        public override void OnStart()
        {
            DialogueManager.Instance.StartDialogue(_dialogue);
            _hasStarted = true;
        }

        public override bool IsObjectiveCompleted()
        {
            return DialogueManager.Instance.DialogueCompleted;
        }
    }
}
