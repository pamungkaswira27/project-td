using JSAM;
using UnityEngine;

namespace ProjectTD
{
    public class BaseObjective : MonoBehaviour
    {
        protected bool _hasStarted;

        public bool HasStarted => _hasStarted;

        public virtual void Initialize() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual bool IsObjectiveCompleted() { return false; }
    }
}
