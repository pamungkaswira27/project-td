using System.Collections.Generic;
using UnityEngine;

namespace ProjectTD
{
    public class RespawnerManager : MonoBehaviour
    {
        public static RespawnerManager Instance;

        [SerializeField]
        private List<Respawner> _objectToRespawnList;
        [SerializeField]
        private List<Objective> _objectiveToReset;
        [SerializeField]
        private List<DialogueTrigger> _dialogueToReset;

        private void Awake()
        {
            Instance = this;
        }

        public void AddObjectToRespawn(Respawner objectToRespawn)
        {
            _objectToRespawnList.Add(objectToRespawn);
        }

        public void AddObjectiveToReset(Objective objectiveToReset)
        {
            _objectiveToReset.Add(objectiveToReset);
        }

        public void AddDialogueToReset(DialogueTrigger dialogue)
        {
            _dialogueToReset.Add(dialogue);
        }

        public void RespawnObjects()
        {
            foreach (Respawner objectToRespawn in _objectToRespawnList)
            {
                objectToRespawn.Respawn();
            }
        }

        public void ResetObjective()
        {
            foreach (Objective objectiveToReset in _objectiveToReset)
            {
                if (objectiveToReset is CollectObjective)
                {
                    CollectObjective objective = objectiveToReset as CollectObjective;
                    objective.ResetProgress();
                    continue;
                }

                if (objectiveToReset is ExploreObjective)
                {
                    ExploreObjective objective = objectiveToReset as ExploreObjective;
                    objective.ResetProgress();
                    continue;
                }

                if (objectiveToReset is KillObjective)
                {
                    KillObjective objective = objectiveToReset as KillObjective;
                    objective.ResetProgress();
                    continue;
                }
            }
        }

        public void ResetDialogue()
        {
            foreach (DialogueTrigger dialogueToReset in _dialogueToReset)
            {
                dialogueToReset.ResetDialogueTrigger();
            }
        }

        public void ClearList()
        {
            foreach (Respawner objectToRespawn in _objectToRespawnList.ToArray())
            {
                if (!objectToRespawn.gameObject.activeInHierarchy)
                {
                    _objectToRespawnList.Remove(objectToRespawn);
                }
            }

            foreach (Objective objectiveToReset in _objectiveToReset.ToArray())
            {
                if (objectiveToReset.IsCompleted)
                {
                    _objectiveToReset.Remove(objectiveToReset);
                }
            }

            foreach (DialogueTrigger dialogueToReset in _dialogueToReset.ToArray())
            {
                if (dialogueToReset.HasTriggered)
                {
                    _dialogueToReset.Remove(dialogueToReset);
                }
            }
        }
    }
}
