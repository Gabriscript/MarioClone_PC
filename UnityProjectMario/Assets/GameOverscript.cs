using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverscript : MonoBehaviour
{
    public static bool GameIsOver = false;
    public GameObject gameOverMenuUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void NewGame() {
        SceneManager.LoadScene("1-1");
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
    }
    public void GameOver() {
       gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;
    }
    public void QuitGame() {
        Application.Quit();
    }
}
