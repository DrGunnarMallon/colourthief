using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ColorManager colorManager;

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

    public void LoadGame()
    {
        ResetScore();
        SceneManager.LoadScene("GameScene");
    }

    public void GoToMainMenu()
    {
        // Reset UIManager
        // Reset PlayerController
        // Reset BubbleController
        // Reset LevelManager
        // Load MainMenu Scene
        levelManager.ResetLevel();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        ResetScore();
        levelManager.NewLevel();
        UIManager.Instance.HideLevelCompleteText();
    }

    public void LevelComplete()
    {
        StartCoroutine(levelManager.LevelComplete());
    }

    #endregion

    // LoadGame

    // NextLevel

    // NewLevel

    // MainMenu

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
