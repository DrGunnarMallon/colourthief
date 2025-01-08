using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Level Complete Text")]
    [SerializeField] private TextMeshPro levelCompleteText;

    [Header("Panel Controllers")]
    [SerializeField] private ContainerController mixingController;
    [SerializeField] private TargetController targetController;
    [SerializeField] private StreakTextController streakController;

    #region Class Setup Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnScoreChanged += UpdateScoreText;
        EventsManager.Instance.OnTargetChanged += UpdateTargetColor;
        EventsManager.Instance.OnLevelCompleted += LevelCompleted;
        EventsManager.Instance.OnNewLevel += NewLevel;
        EventsManager.Instance.OnNextLevel += ResetMixingContainer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnScoreChanged -= UpdateScoreText;
        EventsManager.Instance.OnTargetChanged -= UpdateTargetColor;
        EventsManager.Instance.OnLevelCompleted -= LevelCompleted;
        EventsManager.Instance.OnNewLevel -= NewLevel;
        EventsManager.Instance.OnNextLevel -= ResetMixingContainer;
    }

    #endregion

    #region Public Methods

    public void NewLevel()
    {
        ResetMixingContainer();
        HideLevelCompleteText();
    }

    public void UpdateScoreText(int score)
    {
        streakController.UpdateStreakText(score);
    }

    public void UpdateTargetColor(ColorData color)
    {
        targetController.SetTargetColor(color);
    }

    public void ShowLevelCompleteText()
    {
        levelCompleteText.gameObject.SetActive(true);
    }

    public void HideLevelCompleteText()
    {
        levelCompleteText.gameObject.SetActive(false);
    }

    public void ResetTarget()
    {
        targetController.ResetTarget();
    }

    public void ResetMixingContainer()
    {
        mixingController.ResetContainer();
    }


    private void LevelCompleted()
    {
        // ResetTarget();
        // ResetMixingContainer();
    }

    #endregion
}
