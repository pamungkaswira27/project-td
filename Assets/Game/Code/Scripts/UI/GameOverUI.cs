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
            SceneManager.LoadScene("DemoScene2");
        }

        public void Quit()
        {
            SceneManager.LoadScene("DemoMainMenu");
        }

        private void DisplayGameOverUI()
        {
            _gameOverCanvas.enabled = true;
        }
    }
}
