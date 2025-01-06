using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Managers")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ScoreManager scoreManager;

    #region Startup

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

    #endregion

    #region Game Management Methods

    public void RestartGame()
    {
        levelManager.NewLevel();
        uiManager.HideLevelCompleteText();
    }

    public void LevelComplete()
    {
        StartCoroutine(levelManager.LevelComplete());
    }


    // LoadGame
    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadNextLevel()
    {
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

    #endregion

    #region Scene Management

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #endregion
}
