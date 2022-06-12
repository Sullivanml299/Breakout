using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI livesLabel;

    static public int score;
    static public int lives;
    private int maxScore = 60;

    private void Start()
    {
        score = 0;
        lives = 3;

        scoreLabel.text = "SCORE: " + score.ToString();
        livesLabel.text = "LIVES: " + lives.ToString();
    }

    public void addOne()
    {
        score += 1;
        scoreLabel.text = "SCORE: " + score.ToString();
        if (score >= maxScore) vicotry();
    }

    private void vicotry()
    {
        Victory.score = score;
        Victory.lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Victory");
    }

    public void lifeDown()
    {
        if(lives > 0)
        {
            lives--;
            livesLabel.text = "LIVES: " + lives.ToString();
        }
        else
        {
            GameOverMenu.score = score;
            GameOverMenu.lastScene = SceneManager.GetActiveScene().name;
            //GAMEOVER
            SceneManager.LoadScene("GameOver");
        }
    }
}
