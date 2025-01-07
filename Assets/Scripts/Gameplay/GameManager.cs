using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Singleton Pattern

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

    public void LoadNewLevel()
    {
        Debug.Log("New Level Loaded");
        EventsManager.Instance.TriggerNewLevel();
    }

    public void LevelComplete()
    {
        Debug.Log("Level Complete");
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    public void LoadNextLevel()
    {
        EventsManager.Instance.TriggerNextLevel();
    }

    public void LoadMainMenu()
    {
        EventsManager.Instance.TriggerReturnToMenu();
        SwitchScene("MainMenu");
    }

    public void CheckColorMatch(ColorData color) { }

    #endregion

    #region Scene Management

    public void SwitchScene(string sceneName)
    {
        StartCoroutine(SwitchSceneAsync(sceneName));
    }

    private IEnumerator SwitchSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    private IEnumerator LoadGameSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LoadNewLevel();
    }

    #endregion
}