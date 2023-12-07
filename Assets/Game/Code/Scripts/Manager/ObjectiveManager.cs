using UnityEngine;

namespace ProjectTD
{
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance;

        [SerializeField]
        private BaseObjective[] _objectives;

        private int _activeObjectiveIndex = 0;
        private bool _isAllObjectiveClear;

        public bool IsAllObjectiveClear => _isAllObjectiveClear;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            for (int i = 0; i <  _objectives.Length; i++)
            {
                if (_objectives[i] == null)
                {
                    continue;
                }

                _objectives[i].Initialize();
            }
        }

        private void Update()
        {
            if (_activeObjectiveIndex >= _objectives.Length)
            {
                _isAllObjectiveClear = true;
                Debug.Log($"[{nameof(ObjectiveManager)}]: There's no objective left");
                return;
            }

            BaseObjective objective = _objectives[_activeObjectiveIndex];

            if (objective == null)
            {
                return;
            }

            if (!objective.HasStarted)
            {
                objective.OnStart();
            }

            objective.OnUpdate();

            if (objective.IsObjectiveCompleted())
            {
                _activeObjectiveIndex++;
            }
        }
    }
}
