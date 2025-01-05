using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Image currentColorPanel;
    [SerializeField] private TextMeshProUGUI currentColorText;
    [SerializeField] private TextMeshPro levelCompleteText;

    [SerializeField] private Image targetColorPanel;
    [SerializeField] private TextMeshProUGUI targetColorText;

    [SerializeField] private ContainerController containerController;
    [SerializeField] private TargetController targetController;

    [SerializeField] private TextMeshProUGUI scoreText;


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

    private void Start()
    {
        levelCompleteText.gameObject.SetActive(false);
    }

    #region Public Methods

    public void ShowLevelCompleteText()
    {
        levelCompleteText.gameObject.SetActive(true);
    }

    public void HideLevelCompleteText()
    {
        levelCompleteText.gameObject.SetActive(false);
    }

    public void ClearCurrentColor()
    {
        UpdatePanelColor(ColorManager.Instance.GetColorByName("Gray"));
        UpdatePanelText("None");
    }

    public void UpdatePanelColor(ColorData newColor)
    {
        if (currentColorPanel == null) return;

        currentColorPanel.color = newColor.colorRGB;
    }

    public void UpdatePanelText(string text)
    {
        currentColorText.text = text;
    }

    public void UpdateTargetColor(ColorData newColor)
    {
        targetColorPanel.color = newColor.colorRGB;
        targetColorText.text = newColor.colorName;
        targetController.SetTargetColor(newColor);
    }

    public ColorData GetTargetColor()
    {
        return targetController.GetTargetColor();
    }

    public void ResetContainer()
    {
        containerController.ResetContainer();
        ClearCurrentColor();
    }

    public void ResetUI()
    {
        ResetContainer();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Streak: " + score.ToString();
    }

    #endregion
}
