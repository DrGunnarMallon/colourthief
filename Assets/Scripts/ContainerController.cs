using UnityEngine;
using TMPro;

public class ContainerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI colorNameText;

    private ColorData currentColor = null;
    private int[] bwryb = new int[5];

    #region Private Methods

    private ColorData AddColor(ColorData newColor)
    {
        int[] bwrybValue = newColor.bwryb;

        for (int i = 0; i < 5; i++)
        {
            bwryb[i] += bwrybValue[i];
        }

        ColorData returnColor = ColorManager.Instance.GetColorExact(bwryb);
        if (returnColor == null)
        {
            returnColor = ColorManager.Instance.GetColorNearest(bwryb);
        }

        if (returnColor.colorName == UIManager.Instance.GetTargetColor().colorName)
        {
            GameManager.Instance.LevelComplete();
            bwryb = new int[5];
        }

        return returnColor;
    }

    private void UpdatePanel(ColorData newColor)
    {
        UIManager.Instance.UpdatePanelColor(newColor);
        string colorName = currentColor == null ? "None" : newColor.colorName;
        UIManager.Instance.UpdatePanelText(colorName);
    }

    private void ClearPanel()
    {
        UIManager.Instance.UpdatePanelColor(ColorManager.Instance.GetColorByName("Gray"));
        UIManager.Instance.UpdatePanelText("None");
    }

    #endregion

    #region Public Methods

    public void ReceiveColor(ColorData newColor)
    {
        ColorData color = AddColor(newColor);
        currentColor = color;
        UpdatePanel(currentColor);
    }

    public ColorData ReturnColor()
    {
        ClearPanel();
        ColorData tempColor = currentColor;
        currentColor = null;
        bwryb = new int[5];
        return tempColor;
    }

    public void ResetContainer()
    {
        ClearPanel();
        currentColor = null;
        bwryb = new int[5];
    }

    #endregion
}
