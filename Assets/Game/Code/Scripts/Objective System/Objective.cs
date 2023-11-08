using UnityEngine;

namespace ProjectTD
{
    public abstract class Objective : MonoBehaviour
    {
        [Header("General")]
        [SerializeField]
        private string _description;
        public string Description => _description;
        [SerializeField]
        private bool _isCompleted;
        public bool IsCompleted => _isCompleted;

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
