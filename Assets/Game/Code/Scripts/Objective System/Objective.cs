using UnityEngine;

namespace ProjectTD
{
    public abstract class Objective : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        protected string _description;
        public string Description => _description;
        [SerializeField]
        protected bool _isCompleted;
        public bool IsCompleted => _isCompleted;

        private void Start()
        {
            RespawnerManager.Instance.AddObjectiveToReset(this);
        }

        public void Complete()
        {
            _isCompleted = true;
        }

        public virtual bool IsObjectiveCompleted() 
        { 
            return false; 
        }
    }
}
