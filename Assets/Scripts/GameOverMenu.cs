using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverMenu : MonoBehaviour
{
    public static int score;
    public static string lastScene;
    Button RetryButton;
    Label scoreLabel;

    private void OnEnable()
    {
        Debug.Log("Enabled Game Over Menu");
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        RetryButton = root.Q<Button>("RetryButton");
        RetryButton.clicked += () =>
        {
            retryGame();
        };

        scoreLabel = root.Q<Label>("Score");
        scoreLabel.text = score.ToString();
    }

    private void retryGame()
    {
        if (lastScene != null)
        {
            Debug.Log("Retry Game!");
            RetryButton.SetEnabled(false);
            SceneManager.LoadScene(lastScene, LoadSceneMode.Single);
        }
    }
}
