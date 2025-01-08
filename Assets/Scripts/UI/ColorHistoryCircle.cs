using UnityEngine;
// using UnityEngine.UI;

public class ColorHistoryCircle : MonoBehaviour
{
    private ColorData currentColor;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(ColorData color)
    {
        currentColor = color;
        spriteRenderer.color = color.colorRGB;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public ColorData GetColor()
    {
        return currentColor;
    }
}
