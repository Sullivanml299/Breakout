using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    Label scoreLabel;
    Label livesLabel;
    static public int score;
    static public int lives;

    private void Start()
    {
        score = 0;
        lives = 3;
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        scoreLabel = root.Q<Label>("ScoreLabel");
        livesLabel = root.Q<Label>("LivesLabel");

        scoreLabel.text = "SCORE: " + score.ToString();
        livesLabel.text = "LIVES: " + lives.ToString();
    }

    public void addOne()
    {
        score += 1;
        scoreLabel.text = "SCORE: " + score.ToString();
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
