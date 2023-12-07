using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTD
{
    public class GameClearUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas _gameClearCanvas;

        private void Update()
        {
            if (!ObjectiveManager.Instance.IsAllObjectiveClear)
            {
                return;
            }

            DisplayGameClearUI();
        }

        public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void DisplayGameClearUI()
        {
            InputManager.Instance.DisablePlayerInput();
            _gameClearCanvas.enabled = true;
        }
    }
}
