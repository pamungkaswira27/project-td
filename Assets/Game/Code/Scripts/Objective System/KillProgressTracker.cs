using UnityEngine;

namespace ProjectTD
{
    public class KillProgressTracker : MonoBehaviour
    {
        private void OnDisable()
        {
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            KillObjective currentObjective = ObjectiveManager.Instance.ActiveObjective as KillObjective;

            if (currentObjective == null)
            {
                return;
            }

            currentObjective.UpdateCurrentAmount();

            if (currentObjective.IsObjectiveCompleted())
            {
                ObjectiveManager.Instance.ActiveObjective.Complete();
                ObjectiveManager.Instance.SetActiveObjective();
                Debug.Log($"[{nameof(KillProgressTracker)}]: Objective Completed!");
            }
        }
    }
}
