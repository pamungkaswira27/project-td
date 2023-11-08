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

        private void Start()
        {
            SetActiveObjective();
        }

        public bool HasActiveObjective()
        {
            return (ActiveObjective != null);
        }

        public void SetActiveObjective()
        {
            _activeObjective = null;

            for (int i = 0; i < transform.childCount; i++)
            {
                Objective objective = transform.GetChild(i).GetComponent<Objective>();

                if (objective == null || objective.IsCompleted)
                {
                    continue;
                }

                _activeObjective = objective;
                break;
            }
        }
    }
}
