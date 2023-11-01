using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {   //Memainkan Scene Selanjutnya
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("GameMenuScene");
    }

    public void GoToCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void GoToSelectLevelScene()
    {
        SceneManager.LoadScene("SelectLevelScene");
    }

    public void GoToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
