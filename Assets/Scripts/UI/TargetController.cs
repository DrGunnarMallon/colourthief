using TMPro;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI colorNameText;

    private ColorData targetColor;

    public void SetTargetColor(ColorData newColor)
    {
        targetColor = newColor;
        colorNameText.text = targetColor.colorName;
    }

    public ColorData GetTargetColor()
    {
        return targetColor;
    }
}
