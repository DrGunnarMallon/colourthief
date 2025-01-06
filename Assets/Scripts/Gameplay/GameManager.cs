using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region Game Management Methods

    public void RestartGame()
    {
        ResetScore();
        levelManager.NewLevel();
        uiManager.HideLevelCompleteText();
    }

    public void LevelComplete()
    {
        StartCoroutine(levelManager.LevelComplete());
    }

    #endregion

    // LoadGame
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadNextLevel()
    {
        score++;
        // Reset PlayerController
        // Reset ContainerController
        // Reset UIManager

        // LevelManager Next Level (Color)
    }

    // NewLevel
    public void ReloadLevel()
    {
        // Reset UIManager
        // Reset PlayerController
        // Reset BubbleController
        // Reset ContainerController
        // Reset LevelManager
        score = 0;
        // Level Manager Generate New Level
    }

    // MainMenu
    public void LoadMainMenu()
    {
        // Reset UIManager

        // Reset PlayerController

        // Reset BubbleController

        // Reset ContainerController

        // Reset LevelManager
        levelManager.ResetLevel();
        // Load MainMenu Scene
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #region Scoring Methods

    public void ResetScore()
    {
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
    }

    public int GetScore() => score;

    #endregion
}
