using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject audioManagerPrefab;

    private GameObject bootstrapCamera;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindFirstObjectByType<GameManager>() == null)
        {
            Instantiate(gameManagerPrefab);
        }

        if (FindFirstObjectByType<AudioManager>() == null)
        {
            Instantiate(audioManagerPrefab);
        }

        bootstrapCamera = GameObject.FindGameObjectWithTag("BootstrapCamera");
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!bootstrapCamera) return;

        Camera mainSceneCamera = FindFirstObjectByType<Camera>();

        if (mainSceneCamera != null && bootstrapCamera != null && mainSceneCamera != bootstrapCamera)
        {
            Destroy(bootstrapCamera);
            bootstrapCamera = null;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
