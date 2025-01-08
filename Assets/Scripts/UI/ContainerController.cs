using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContainerController : MonoBehaviour
{
    [SerializeField] private ColorManager colorManager;

    private ColorData currentColor = null;
    private int[] bwryb = new int[5];

    private TextMeshProUGUI colorNameText;
    private Image targetPanel;

    private void Awake()
    {
        colorNameText = GetComponentInChildren<TextMeshProUGUI>();
        targetPanel = GetComponent<Image>();
    }

    #region Color Container Methods

    public void AddMixingColor(ColorData newColor)
    {
        if (newColor == null) return;

        currentColor = colorManager.MixColors(bwryb, newColor.bwryb);
        for (int i = 0; i < 5; i++)
        {
            bwryb[i] += newColor.bwryb[i];
        }
        targetPanel.color = currentColor.colorRGB;
        colorNameText.text = currentColor.colorName;

        AudioManager.Instance.PlaySound(AudioManager.AudioType.MixPaint);

        EventsManager.Instance.TriggerAddColorToHistory(newColor);
        EventsManager.Instance.TriggerMixingColorChanged(currentColor);
    }

    public void ResetContainer()
    {
        currentColor = null;
        bwryb = new int[5];
        targetPanel.color = Color.gray;
        colorNameText.text = "None";
    }

    public ColorData GetCurrentColor() => currentColor;

    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ColorData newColor = other.GetComponent<PlayerController>().DrainColor();
            AddMixingColor(newColor);
        }
    }
}
