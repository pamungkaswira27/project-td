using NaughtyAttributes;
using UnityEngine;

namespace ProjectTD
{
    public abstract class Objective : MonoBehaviour
    {
        [Header("General")]
        [SerializeField, TextArea(2, 4)]
        protected string _description;
        public string Description => _description;
        [SerializeField, ReadOnly]
        protected bool _isCompleted;
        public bool IsCompleted => _isCompleted;
        [SerializeField]
        protected bool _autoTriggerNextObjective = true;

        private void Start()
        {
            RespawnerManager.Instance.AddObjectiveToReset(this);
        }

        public void Complete()
        {
            _isCompleted = true;
            ObjectiveManager.Instance.ClearActiveObjective();
            DialogueManager.Instance.ClearSentence();

            if (_autoTriggerNextObjective)
            {
                ObjectiveManager.Instance.SetActiveObjective();
            }
        }

        public virtual bool IsObjectiveCompleted() 
        { 
            return false; 
        }
    }
}
