using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Level Complete Text")]
    [SerializeField] private TextMeshPro levelCompleteText;
    [SerializeField] private TextMeshPro levelFailedText;
    [SerializeField] private GameObject firstLaunchPanel;

    [Header("Panel Controllers")]
    [SerializeField] private ContainerController mixingController;
    [SerializeField] private TargetController targetController;
    [SerializeField] private StreakTextController streakController;
    [SerializeField] private ColorHistoryManager colorHistoryController;

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

    private void Start()
    {
        HideFirstLaunchText();
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnScoreChanged += UpdateScoreText;
        EventsManager.Instance.OnTargetChanged += UpdateTargetColor;
        // EventsManager.Instance.OnLevelCompleted += LevelCompleted;
        EventsManager.Instance.OnNewLevel += NewLevel;
        EventsManager.Instance.OnNextLevel += ResetMixingContainer;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnScoreChanged -= UpdateScoreText;
        EventsManager.Instance.OnTargetChanged -= UpdateTargetColor;
        // EventsManager.Instance.OnLevelCompleted -= LevelCompleted;
        EventsManager.Instance.OnNewLevel -= NewLevel;
        EventsManager.Instance.OnNextLevel -= ResetMixingContainer;
    }

    #endregion

    #region Public Methods

    public void NewLevel()
    {
        ResetMixingContainer();
        HideLevelCompleteText();
        HideLevelFailedText();
    }

    public void UpdateScoreText(int score)
    {
        streakController.UpdateStreakText(score);
    }

    public void UpdateTargetColor(ColorData color)
    {
        targetController.SetTargetColor(color);
        colorHistoryController.CreateCircles(color.NumberOfColors());
    }

    public void ShowLevelCompleteText()
    {
        levelCompleteText.gameObject.SetActive(true);
    }

    public void HideLevelCompleteText()
    {
        levelCompleteText.gameObject.SetActive(false);
    }

    public void ShowLevelFailedText()
    {
        levelFailedText.gameObject.SetActive(true);
    }

    public void HideLevelFailedText()
    {
        levelFailedText.gameObject.SetActive(false);
    }

    public void ResetTarget()
    {
        targetController.ResetTarget();
    }

    public void ResetMixingContainer()
    {
        mixingController.ResetContainer();
    }

    public void ShowFirstLaunchText()
    {
        Debug.Log("Show First Launch");
        firstLaunchPanel.SetActive(true);
    }

    public void HideFirstLaunchText()
    {
        Debug.Log("Show First Launch");
        firstLaunchPanel.SetActive(false);
    }

    #endregion
}
