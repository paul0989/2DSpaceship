using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
    public Text scoreText;
    private int totalScore = 0;
    public GameObject gameOverUI;
    

    private void SetScoreText(int score)
    {
        totalScore += score;
        scoreText.text = "Score：" + totalScore+"";
    }

    private void ShowGameOver()
    //太空船爆炸後顯示GameOver的UI
    {
        gameOverUI.active = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //遊戲當中的場景名稱get正在活躍active的scene
    }

    private void OnEnable()
    {
        Enemy.ExplodinEvent += SetScoreText;
        //爆炸呼叫SetScoreText
        ShipController.GameOverEvent += ShowGameOver;
    }
    private void OnDisable()
    {
        Enemy.ExplodinEvent -= SetScoreText;
        ShipController.GameOverEvent -= ShowGameOver;
    }
}
