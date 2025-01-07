using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContainerController : MonoBehaviour
{
    [SerializeField] private ColorManager colorManager;

    private TextMeshProUGUI colorNameText;
    private Image containerPanel;
    private ColorData currentColor = null;
    private int[] bwryb = new int[5];

    #region Private Methods

    private void Awake()
    {
        colorNameText = GetComponentInChildren<TextMeshProUGUI>();
        containerPanel = GetComponent<Image>();
    }

    #endregion

    #region Color Container Methods

    public void AddMixingColor(ColorData newColor)
    {
        currentColor = colorManager.MixColors(bwryb, newColor.bwryb);
        containerPanel.color = currentColor.colorRGB;
        colorNameText.text = currentColor.colorName;
    }

    public void ResetContainer()
    {
        currentColor = null;
        bwryb = new int[5];
        containerPanel.color = Color.gray;
        colorNameText.text = "None";
    }

    public ColorData GetCurrentColor()
    {
        return currentColor;
    }

    #endregion
}
