using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    private TextMeshProUGUI colorNameText;
    private Image targetPanel;

    private ColorData targetColor;

    private void Awake()
    {
        colorNameText = GetComponentInChildren<TextMeshProUGUI>();
        targetPanel = GetComponentInChildren<Image>();
    }

    public void SetTargetColor(ColorData newColor)
    {
        targetColor = newColor;
        colorNameText.text = targetColor.colorName;
        targetPanel.color = targetColor.colorRGB;
    }

    public void ResetTarget()
    {
        targetColor = null;
        colorNameText.text = "None";
        targetPanel.color = Color.gray;
    }

    public ColorData GetTargetColor()
    {
        return targetColor;
    }
}
