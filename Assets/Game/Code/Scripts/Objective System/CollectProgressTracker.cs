using UnityEngine;

namespace ProjectTD
{
    public class CollectProgressTracker : MonoBehaviour
    {
        private void OnDisable()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            CollectObjective currentObjective = ObjectiveManager.Instance.ActiveObjective as CollectObjective;

            if (currentObjective == null)
            {
                return;
            }

            currentObjective.UpdateCurrentAmount();

            if (currentObjective.IsObjectiveCompleted())
            {
                ObjectiveManager.Instance.ActiveObjective.Complete();
                Debug.Log($"[{nameof(CollectProgressTracker)}]: Objective Completed!");
            }
        }
    }
}
