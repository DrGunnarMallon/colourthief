using TMPro;
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

    [Header("Score Text")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Color Manager")]
    [SerializeField] private ColorManager colorManager;

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
        levelCompleteText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventsManager.Instance.OnScoreChanged += UpdateScoreText;
        EventsManager.Instance.OnTargetColorChanged += UpdateTargetColor;
        EventsManager.Instance.OnMixingColorChanged += UpdateMixingContainerColor;
        EventsManager.Instance.OnLevelCompleted += LevelCompleted;

        EventsManager.Instance.OnResetGame += ResetAll;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnScoreChanged -= UpdateScoreText;
        EventsManager.Instance.OnTargetColorChanged -= UpdateTargetColor;
        EventsManager.Instance.OnMixingColorChanged -= UpdateMixingContainerColor;
        EventsManager.Instance.OnLevelCompleted -= LevelCompleted;

        EventsManager.Instance.OnResetGame -= ResetAll;
    }

    #endregion

    #region Public Methods

    public void ResetAll()
    {
        ResetTarget();
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

    public void UpdateMixingContainerColor(ColorData data)
    {
        mixingController.AddMixingColor(data);
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
        ResetTarget();
        ResetMixingContainer();
        HideLevelCompleteText();
    }

    #endregion
}
