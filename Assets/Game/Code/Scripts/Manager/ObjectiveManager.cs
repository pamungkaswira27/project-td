using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;

        [SerializeField, ReadOnly]
        private Objective _activeObjective;
        public Objective ActiveObjective => _activeObjective;

        private void Awake()
        {
            Instance = this;
        }

        public bool HasActiveObjective()
        {
            return (ActiveObjective != null);
        }

        public void ClearActiveObjective()
        {
            _activeObjective = null;
        }

        public void SetActiveObjective()
        {
            _activeObjective = null;

            for (int i = 0; i < transform.childCount; i++)
            {
                Objective objective = transform.GetChild(i).GetComponent<Objective>();

                if (objective.IsCompleted)
                {
                    continue;
                }

                _activeObjective = objective;
                break;
            }

            if (_activeObjective == null)
            {
                return;
            }

            if (_activeObjective.TryGetComponent(out DialogueTrigger dialogueTrigger))
            {
                InputManager.Instance.DisablePlayerInput();
                dialogueTrigger.TriggerDialogue();
            }
        }
    }
}
