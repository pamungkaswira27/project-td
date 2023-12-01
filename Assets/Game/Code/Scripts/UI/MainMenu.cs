using JSAM;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectTD
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.StopAllMusic();
            AudioManager.PlayMusic(MainMusic.main_menu_themes, false);
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}