using UnityEngine;
using TMPro;

public class StreakTextController : MonoBehaviour
{
    private TextMeshProUGUI streakText;

    private void Awake()
    {
        streakText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateStreakText(int score)
    {
        streakText.text = "Streak: " + score.ToString();
    }
}
