using UnityEngine;

namespace ProjectTD
{
    public class ExploreProgressTracker : MonoBehaviour
    {
        private void Update()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            ExploreObjective currentObjective = ObjectiveManager.Instance.ActiveObjective as ExploreObjective;

            if (currentObjective == null)
            {
                return;
            }

            if (currentObjective.IsObjectiveCompleted())
            {
                ObjectiveManager.Instance.ActiveObjective.Complete();
                ObjectiveManager.Instance.SetActiveObjective();
                Debug.Log($"[{nameof(ExploreProgressTracker)}]: Objective Completed!");
                return;
            }
        }
    }
}
