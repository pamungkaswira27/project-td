using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTD
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField]
        private Canvas _gameOverCanvas;

        private void Update()
        {
            if (PlayerManager.Instance.Life > 0)
            {
                return;
            }

            DisplayGameOverUI();
        }

        public void Retry()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void DisplayGameOverUI()
        {
            _gameOverCanvas.enabled = true;
        }
    }
}
